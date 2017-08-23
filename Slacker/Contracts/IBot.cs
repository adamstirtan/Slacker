using System.Threading.Tasks;

using SlackConnector.Models;

namespace Slacker.Contracts
{
    public interface IBot
    {
        Task OnMessageReceived(SlackMessage message);

        Task OnUserJoined(SlackUser user);

        void OnDisconnected();

        Task OnReconnect();
    }
}
