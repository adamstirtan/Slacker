using System.Threading.Tasks;

using SlackConnector.Models;

namespace Slacker.Contracts
{
    public interface IBot
    {
        IBotConfiguration GetBotConfiguration(string fileName = "");

        Task OnMessageReceived(SlackMessage message);

        void OnDisconnected();
    }
}
