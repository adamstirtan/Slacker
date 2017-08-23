using System;

namespace Slacker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bot = new SlackerBot();
            var configuration = bot.GetBotConfiguration();

            var connector = new SlackConnector.SlackConnector();
            var connection = connector.Connect(configuration.SlackToken).GetAwaiter().GetResult();

            connection.OnMessageReceived += bot.OnMessageReceived;
            connection.OnDisconnect += bot.OnDisconnected;

            Console.ReadKey();
        }
    }
}