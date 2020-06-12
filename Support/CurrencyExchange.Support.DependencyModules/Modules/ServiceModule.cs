using CurrencyExchange.Business.Interfaces;
using CurrencyExchange.Business.Services;
using CurrencyExchange.Business.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchange.Support.DependencyModules
{
    public static class ServiceModule
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
        }
    }
}
