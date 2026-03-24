using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Core.Extensions
{
    public static class CoreServicesExtension
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            // Register core services here
            services.AddSingleton<IAppRepository, DataRepository>(sp =>
    new DataRepository("app.db"));
            
            // Provide a default IConfiguration so services depending on IConfiguration can be resolved in tests
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder().AddInMemoryCollection().Build());

            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
