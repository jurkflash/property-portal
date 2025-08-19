using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Persistence
{
    public class PropertyDbContextFactory : IDesignTimeDbContextFactory<PropertyDbContext>
    {
        public PropertyDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Pokok.PropertyPortal.Api"));
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString("IdentityConnection");

            var optionsBuilder = new DbContextOptionsBuilder<PropertyDbContext>();
            optionsBuilder.UseNpgsql(connectionString, b =>
            {
                b.MigrationsAssembly("Pokok.PropertyPortal.Infrastructure");
            });
            return new PropertyDbContext(optionsBuilder.Options);
        }
    }
}
