using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using CurrencyExchange.Business.Services.Mapping;

namespace CurrencyExchange.Support.DependencyModules
{
    public static class ComponentModule
    {
        public static void Configure(IServiceCollection services)
        {
            var config = ModelMapper.Configure();
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
