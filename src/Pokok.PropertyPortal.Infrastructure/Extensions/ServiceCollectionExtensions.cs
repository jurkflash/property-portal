using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokok.BuildingBlocks.Outbox;
using Pokok.PropertyPortal.Infrastructure.Outbox.Persistence;
using Pokok.PropertyPortal.Infrastructure.Properties.Extensions;

namespace Pokok.PropertyPortal.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOutbox(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PropertyOutboxDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PropertiesConnection")));
            services.AddScoped<OutboxDbContext>(sp => sp.GetRequiredService<PropertyOutboxDbContext>());
            services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();
            services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));
            services.AddOutboxProcessor<PropertyOutboxDbContext>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddProperties(configuration);
            services.AddOutbox(configuration);
            return services;
        }
    }
}
