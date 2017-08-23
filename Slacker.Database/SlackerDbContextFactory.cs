using System.IO;

using Newtonsoft.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Slacker.Database
{
    public class SlackerDbContextFactory : IDesignTimeDbContextFactory<SlackerContext>
    {
        private const string ConnectionStringFileName = "connectionString.json";

        public SlackerContext CreateDbContext(string[] args)
        {
            ConnectionStringConfiguration configuration;

            if (!File.Exists(ConnectionStringFileName))
            {
                throw new FileNotFoundException($"Could not find the {ConnectionStringFileName}. Ensure that the example file is renamed and populated with your connection string.");
            }

            using (var streamReader = new StreamReader(ConnectionStringFileName))
            {
                var json = streamReader.ReadToEnd();

                configuration = JsonConvert.DeserializeObject<ConnectionStringConfiguration>(json);
            }

            var optionsBuilder = new DbContextOptionsBuilder<SlackerContext>();

            optionsBuilder.UseSqlServer(configuration.ConnectionString);

            return new SlackerContext(optionsBuilder.Options);
        }
    }
}
