using Microsoft.EntityFrameworkCore;

namespace DockerizedAppSample
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Label> Labels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConfigurationProvider.DbConnectionString);
        }
    }
}
