using System;
using System.Threading.Tasks;

using SlackConnector.Models;
using Slacker.Contracts;

namespace Slacker
{
    public sealed class SlackerBot : IBot
    {
        public IBotConfiguration GetBotConfiguration(string fileName = "")
        {
            throw new NotImplementedException();
        }

        public Task OnMessageReceived(SlackMessage message)
        {
            throw new NotImplementedException();
        }

        public void OnDisconnected()
        {
            throw new NotImplementedException();
        }
    }
}
