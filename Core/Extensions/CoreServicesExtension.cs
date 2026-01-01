using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            // Register core services here
            services.AddSingleton<IAppRepository, DataRepository>(sp =>
    new DataRepository("app.db"));
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
