using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Pokok.PropertyPortal.Infrastructure.Outbox.Persistence
{
    public class PropertyOutboxDbContextFactory : IDesignTimeDbContextFactory<PropertyOutboxDbContext>
    {
        public PropertyOutboxDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Pokok.PropertyPortal.Api"));
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString("PropertyConnection");

            var optionsBuilder = new DbContextOptionsBuilder<PropertyOutboxDbContext>();
            optionsBuilder.UseNpgsql(connectionString, b =>
            {
                b.MigrationsAssembly("Pokok.PropertyPortal.Infrastructure");
            });
            return new PropertyOutboxDbContext(optionsBuilder.Options);
        }
    }
}
