using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlantCare.API.DataAccess.Repositories.PlaceRepository;
using PlantCare.API.DataAccess.Repositories.PlantRepository;

namespace PlantCare.API.DataAccess;

public static class DataExtensions
{
    public static void Migrate(this IApplicationBuilder app) => DatabaseMigrationService.MigrationInitialization(app);

    public static void SetupDataAccess(this IServiceCollection services)
    {
        services.AddDataContext();
        services.AddRepositories();
    }

    private static void AddDataContext(this IServiceCollection services)
    {
        var connectionString = GetConnectionString();
        services.AddDbContext<PlantContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddDbContext<PlaceContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPlantRepository, PlantRepository>();
        services.AddScoped<IPlaceRepository, PlaceRepository>();
    }

    private static string GetConnectionString()
    {
        var databaseServer = Environment.GetEnvironmentVariable("DatabaseServer");
        var databasePort = Environment.GetEnvironmentVariable("DatabasePort");
        var databaseUser = Environment.GetEnvironmentVariable("DatabaseUser");
        var databasePassword = Environment.GetEnvironmentVariable("DatabasePassword");
        var databaseName = Environment.GetEnvironmentVariable("DatabaseName");

        var connectionString =
            $"Server=172.25.224.1,1433;Database=PlantCare_DB;User Id=sa;Password=Password.1234;TrustServerCertificate=true";
        return connectionString;
    }
}