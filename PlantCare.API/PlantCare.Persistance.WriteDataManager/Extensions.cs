using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlantCare.Persistance.Interfaces;
using PlantCare.Persistance.Interfaces.WriteRepositories;
using PlantCare.Persistance.WriteDataManager.Repositories;

namespace PlantCare.Persistance.WriteDataManager;

public static class Extensions
{
    public static void Migrate(this IApplicationBuilder app) => DatabaseMigrationService.MigrationInitialization(app);

    public static void SetupDataAccess(this IServiceCollection services)
    {
        services.AddDataContext();
        services.AddRepositories();
    }

    public static void SetupCache(this IServiceCollection services)
    {
        var redisConnectionString = $"{Environment.GetEnvironmentVariable("RedisConnectionString")},password={Environment.GetEnvironmentVariable("RedisPassword")}";
        var redisInstance = Environment.GetEnvironmentVariable("RedisInstance");

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = redisInstance;
        });
    }

    private static void AddDataContext(this IServiceCollection services)
    {
        var connectionString = GetConnectionString();
        services.AddDbContext<WriteDataContext>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPlaceContext, WriteDataContext>();
        services.AddScoped<IPlantContext, WriteDataContext>();
        services.AddScoped<IModuleContext, WriteDataContext>();
        services.AddScoped<IHumidityMeasurementContext, WriteDataContext>();

        services.AddScoped<IWritePlantRepository, PlantRepository>();
        services.AddScoped<IWritePlaceRepository, PlaceRepository>();
        services.AddScoped<IWriteModuleRepository, ModuleRepository>();
        services.AddScoped<IWriteHumidityMeasurementRepository, HumidityMeasurementRepository>();
    }

    private static string GetConnectionString()
    {
        var databaseServer = Environment.GetEnvironmentVariable("DatabaseServer");
        var databasePort = Environment.GetEnvironmentVariable("DatabasePort");
        var databaseUser = Environment.GetEnvironmentVariable("DatabaseUser");
        var databasePassword = Environment.GetEnvironmentVariable("DatabasePassword");
        var databaseName = Environment.GetEnvironmentVariable("WriteDatabaseName");

        var connectionString =
            $"Server={databaseServer},{databasePort};Database={databaseName};User Id={databaseUser};Password={databasePassword};TrustServerCertificate=true";
        return connectionString;
    }
}