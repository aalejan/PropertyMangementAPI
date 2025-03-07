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
                Host = "",
                Port = ,
                Database = "",
                Username = "",
                Password = ""
            };

            optionsBuilder.UseNpgsql(builder.ToString());

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}