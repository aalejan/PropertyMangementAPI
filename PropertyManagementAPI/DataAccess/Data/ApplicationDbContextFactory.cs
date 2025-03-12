using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace PropertyManagement.DataAccess.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = "tramway.proxy.rlwy.net",
                Port = 37797,
                Database = "railway",
                Username = "postgres",
                Password = "RqumCoPEQDjjZjmEmUUwoypwcFLdwBFo"
            };

            optionsBuilder.UseNpgsql(builder.ToString());

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}