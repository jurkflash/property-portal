using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pokok.BuildingBlocks.Cqrs.Events;
using Pokok.BuildingBlocks.Persistence;
using Pokok.BuildingBlocks.Persistence.Abstractions;
using Pokok.PropertyPortal.Domain.Parties.Repositories;
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

            services.AddScoped<IUnitOfWork>(sp =>
            {
                var dbContext = sp.GetRequiredService<PropertyDbContext>();
                var dispatcher = sp.GetRequiredService<IDomainEventDispatcher>(); // may be null
                var loggger = sp.GetRequiredService<ILogger<UnitOfWork<PropertyDbContext>>>();
                return new UnitOfWork<PropertyDbContext>(dbContext, dispatcher, loggger);
            });

            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPartyRepository, PartyRepository>();

            return services;
        }
    }
}
