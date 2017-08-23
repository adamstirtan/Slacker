using System.Threading.Tasks;

using SlackConnector;
using SlackConnector.Models;
using Slacker.Database;

namespace Slacker.Contracts
{
    public interface ITrigger
    {
        string Name { get; }

        Task Execute(ITriggerArguments arguments, ISlackConnection connection, SlackChatHub chatHub, SlackerContext context);
    }
}
