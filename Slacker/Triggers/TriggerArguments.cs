using Slacker.Contracts;

namespace Slacker.Triggers
{
    public class TriggerArguments : ITriggerArguments
    {
        public TriggerArguments(string[] arguments)
        {
            Arguments = arguments;
        }

        public string[] Arguments { get; set; }
    }
}
