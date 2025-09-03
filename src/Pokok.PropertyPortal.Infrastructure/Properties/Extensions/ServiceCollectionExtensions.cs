using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokok.PropertyPortal.Domain.Properties.Repositories;
using Pokok.PropertyPortal.Infrastructure.Properties.Persistence;
using Pokok.PropertyPortal.Infrastructure.Properties.Repository;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProperties(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PropertyDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PropertiesConnection")));
            services.AddScoped<PropertyDbContext>();
            services.AddScoped<IPropertyRepository>(sp =>
                new PropertyRepository(sp.GetRequiredService<PropertyDbContext>()));

            return services;
        }
    }
}
