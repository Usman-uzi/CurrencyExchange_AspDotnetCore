using CurrencyExchange.Data;
using CurrencyExchange.Data.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CurrencyExchange.Support.DependencyModules
{
    public class RepositoryModule
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Data.DatabaseContext>(
                options => options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    mySqlServerOptions => mySqlServerOptions.MigrationsAssembly("CurrencyExchange.Data")
                    )
            );
            services.AddScoped<IDatabaseContext, DatabaseContext>();
            services.AddScoped<IUnitOfWork>(unitOfWork => new UnitOfWork(unitOfWork.GetService<IDatabaseContext>()));
            services.AddTransient<IAuditRepository, AuditRepository>();
        }

        public static void ConfigureDatabaseDefaults(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Data.DatabaseContext>() as Data.DatabaseContext;
                {
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
