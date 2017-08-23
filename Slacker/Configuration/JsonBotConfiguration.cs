using Slacker.Contracts;

namespace Slacker.Configuration
{
    public class JsonBotConfiguration : IBotConfiguration
    {
        public string SlackToken { get; set; }
    }
}
