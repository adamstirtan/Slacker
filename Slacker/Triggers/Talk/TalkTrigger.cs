using System;
using System.Linq;
using System.Threading.Tasks;

using Markov;

using SlackConnector;
using SlackConnector.Models;

using Slacker.Contracts;
using Slacker.Database;

namespace Slacker.Triggers.Talk
{
    public class TalkTrigger : ITrigger
    {
        public string Name => "!talk";

        public Task Execute(ITriggerArguments arguments, ISlackConnection connection, SlackChatHub chatHub, SlackerContext context)
        {
            if (arguments == null || arguments.Arguments.Length != 1)
            {
                connection.Say(new BotMessage
                {
                    ChatHub = chatHub,
                    Text = "Usage: !talk <user>"
                });

                return Task.CompletedTask;
            }

            var user = arguments.Arguments[0];
            var messages = context.Messages.Where(x => x.User == user && x.Content.Length > 10);

            if (!messages.Any())
            {
                connection.Say(new BotMessage
                {
                    ChatHub = chatHub,
                    Text = $"There aren't enough messages by {arguments.Arguments[0]} to go on"
                });

                return Task.CompletedTask;
            }

            var chain = new MarkovChain<string>(1);

            foreach (var message in messages)
            {
                chain.Add(message.Content.Split(new[] { ' ' }), 1);
            }

            connection.Say(new BotMessage
            {
                ChatHub = chatHub,
                Text = string.Join(" ", chain.Chain(new Random(DateTime.UtcNow.Millisecond)))
            });

            return Task.CompletedTask;
        }
    }
}
