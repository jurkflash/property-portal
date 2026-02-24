using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokok.PropertyPortal.Application.Services;
using Pokok.PropertyPortal.Infrastructure.Identity.Services;

namespace Pokok.PropertyPortal.Infrastructure.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IdentityServerOptions>(configuration.GetSection(IdentityServerOptions.SectionName));

            services.AddHttpClient<IIdentityTokenService, IdentityTokenService>((sp, client) =>
            {
                var options = configuration.GetSection(IdentityServerOptions.SectionName).Get<IdentityServerOptions>();
                client.BaseAddress = new Uri(options!.BaseUrl);
            });

            services.AddHttpClient<IIdentityService, IdentityUserService>((sp, client) =>
            {
                var options = configuration.GetSection(IdentityServerOptions.SectionName).Get<IdentityServerOptions>();
                client.BaseAddress = new Uri(options!.BaseUrl);
            });

            return services;
        }
    }
}
