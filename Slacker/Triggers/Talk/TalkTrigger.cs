using System.Threading.Tasks;

using SlackConnector;
using SlackConnector.Models;

using Slacker.Contracts;
using Slacker.Database;

namespace Slacker.Triggers.Talk
{
    public class TalkTrigger : ITrigger
    {
        public string Name => "!talk";

        public Task Execute(ISlackConnection connection, SlackChatHub chatHub, SlackerContext context)
        {
            connection.Say(new BotMessage
            {
                ChatHub = chatHub,
                Text = "talk command"
            });

            return Task.CompletedTask;
        }
    }
}
