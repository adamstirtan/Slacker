using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly List<string> _messageStarts = new List<string>
        {
            "If I were #USER#, I'd probably say:\n",
            "Oh that son of a bitch, they'd say something like:\r",
            "Watch this it's going sound just like #USER#:\n",
            "#USER# is dumb, I guess he'd say:\n",
            "That's easy, #USER# would say:\n",
            "He always says shit like:\n",
            "Pretending to be a terrible person is easy, watch:\n",
            "That shitface would be like:\n"
        };

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

            var random = new Random(DateTime.UtcNow.Millisecond);

            var stringBuilder = new StringBuilder(GetRandomMessageStart(user, random));

            stringBuilder.Append(">" + string.Join(" ", chain.Chain(random)));

            connection.Say(new BotMessage
            {
                ChatHub = chatHub,
                Text = stringBuilder.ToString()
            });

            return Task.CompletedTask;
        }

        private string GetRandomMessageStart(string user, Random random)
        {
            return _messageStarts[random.Next(_messageStarts.Count)].Replace("#USER#", user);
        }
    }
}
