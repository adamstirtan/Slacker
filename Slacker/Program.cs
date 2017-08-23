using System;
using Slacker.Configuration;

namespace Slacker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configuration = BotConfigurationFactory.ReadConfiguration();

            var connector = new SlackConnector.SlackConnector();
            var connection = connector.Connect(configuration.SlackToken)
                .GetAwaiter()
                .GetResult();

            var bot = new SlackerBot(connection);

            connection.OnMessageReceived += bot.OnMessageReceived;
            connection.OnUserJoined += bot.OnUserJoined;
            connection.OnDisconnect += bot.OnDisconnected;
            connection.OnReconnect += bot.OnReconnect;

            Console.ReadKey();
        }
    }
}