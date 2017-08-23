using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SlackConnector;
using SlackConnector.Models;

using Slacker.Contracts;
using Slacker.Database;
using Slacker.ObjectModel;
using Slacker.Triggers;
using Slacker.Triggers.Talk;

namespace Slacker
{
    public sealed class SlackerBot : IBot
    {
        private readonly ISlackConnection _connection;
        private readonly IDictionary<string, Func<ITriggerArguments, ISlackConnection, SlackChatHub, SlackerContext, Task>> _triggers =
            new Dictionary<string, Func<ITriggerArguments, ISlackConnection, SlackChatHub, SlackerContext, Task>>();

        public SlackerBot(ISlackConnection connection)
        {
            _connection = connection;

            var talkTrigger = new TalkTrigger();
            _triggers.Add(talkTrigger.Name, talkTrigger.Execute);
        }

        public async Task OnMessageReceived(SlackMessage message)
        {
            if (string.IsNullOrEmpty(message?.Text))
            {
                return;
            }

            if (message.Text.StartsWith("!"))
            {
                var split = message.Text.Split(new[] {' '});
                var command = split[0];

                ITriggerArguments arguments = null;

                if (split.Length > 1)
                {
                    arguments = new TriggerArguments(split.Skip(1).Take(split.Length - 1).ToArray());
                }
                
                if (_triggers.Keys.Contains(command))
                {
                    using (var context = new SlackerDbContextFactory().CreateDbContext(null))
                    {
                        await _triggers[command](arguments, _connection, message.ChatHub, context);
                    }
                }
            }
            else
            {
                if (message.User.IsBot)
                {
                    return;
                }

                // ReSharper disable once AssignNullToNotNullAttribute
                using (var context = new SlackerDbContextFactory().CreateDbContext(null))
                {
                    context.Messages.Add(new Message
                    {
                        Content = message.Text,
                        User = message.User.Name,
                        Created = DateTime.UtcNow
                    });

                    await context.SaveChangesAsync();
                }
            }
        }

        public Task OnUserJoined(SlackUser user)
        {
            return Task.CompletedTask;
        }

        public void OnDisconnected()
        { }

        public Task OnReconnect()
        {
            return Task.CompletedTask;
        }
    }
}
