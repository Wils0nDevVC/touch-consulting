using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using JobBoard.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;


namespace TouchConsulting.GestorInventario.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration Configuration, bool IsLocal)
        {
            services.AddDbContext<BaseDbContext>(opstions =>
            {
                opstions.UseSqlServer(Configuration.GetConnectionString("DB_TOUCHCONSULTING"));
                if (IsLocal)
                {
                    opstions.LogTo(Console.WriteLine, LogLevel.Information)
                            .EnableSensitiveDataLogging();
                }
            });

            return services;
        }
    }
}
