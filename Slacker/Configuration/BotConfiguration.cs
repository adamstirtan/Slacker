using Slacker.Contracts;

namespace Slacker.Configuration
{
    public class BotConfiguration : IBotConfiguration
    {
        public string SlackToken { get; set; }
    }
}
