using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TouchConsulting.GestorInventario.Domain.Interfaces;
using TouchConsulting.GestorInventario.Infrastructure.Repositories;
using TouchConsulting.GestorInventario.Application.Interfaces;
using TouchConsulting.GestorInventario.Infrastructure.Persitence;
using TouchConsulting.GestorInventario.Infrastructure.Security;
using TouchConsulting.GestorInventario.Application.Interfaces.Repository;


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

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
