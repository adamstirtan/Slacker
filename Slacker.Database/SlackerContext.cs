using Microsoft.EntityFrameworkCore;

using Slacker.ObjectModel;

namespace Slacker.Database
{
    public class SlackerContext : DbContext
    {
        public SlackerContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Message> Messages { get; set; }
    }
}
