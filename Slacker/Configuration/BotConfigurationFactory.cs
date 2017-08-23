using System.IO;

using Newtonsoft.Json;

using Slacker.Contracts;

namespace Slacker.Configuration
{
    public static class BotConfigurationFactory
    {
        public static string ConfigurationFileName = "botconfiguration.json";

        public static IBotConfiguration ReadConfiguration()
        {
            IBotConfiguration configuration;

            if (!File.Exists(ConfigurationFileName))
            {
                throw new FileNotFoundException($"Could not find {ConfigurationFileName}");
            }

            using (var streamReader = new StreamReader(ConfigurationFileName))
            {
                var json = streamReader.ReadToEnd();

                configuration = JsonConvert.DeserializeObject<BotConfiguration>(json);
            }

            return configuration;
        }
    }
}
