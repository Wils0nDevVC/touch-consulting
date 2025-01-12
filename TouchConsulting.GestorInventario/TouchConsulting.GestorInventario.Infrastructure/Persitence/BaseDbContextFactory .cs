using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TouchConsulting.GestorInventario.Infrastructure.Persitence;

public class BaseDbContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
{
    public BaseDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();

        var configuration = new ConfigurationBuilder()
            // Configura la ruta base para leer desde el directorio de TouchConsulting.GestorInventario.Api
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"..\TouchConsulting.GestorInventario.Api"))
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DB_TOUCHCONSULTING");

        optionsBuilder.UseSqlServer(connectionString);

        return new BaseDbContext(optionsBuilder.Options);
    }
}
