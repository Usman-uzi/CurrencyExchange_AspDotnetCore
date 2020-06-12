using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchange.Support.DependencyModules
{
    public static class Extensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            ServiceModule.Configure(services);
            RepositoryModule.Configure(services, configuration);
            ComponentModule.Configure(services);
        }

        public static void ConfigureDatabaseDefaults(this IApplicationBuilder app)
        {
            RepositoryModule.ConfigureDatabaseDefaults(app);
        }
    }
}
