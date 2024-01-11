using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlantCare.Persistance.Interfaces;
using PlantCare.Persistance.ReadDataManager.CacheRepositories;
using PlantCare.Persistance.ReadDataManager.Repositories.HumidityMeasurementRepository;
using PlantCare.Persistance.ReadDataManager.Repositories.ModelRepository;
using PlantCare.Persistance.ReadDataManager.Repositories.PlaceRepository;
using PlantCare.Persistance.ReadDataManager.Repositories.PlantRepository;

namespace PlantCare.Persistance.ReadDataManager;

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
        services.AddDbContext<ReadDataContext>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPlaceContext, ReadDataContext>();
        services.AddScoped<IPlantContext, ReadDataContext>();
        services.AddScoped<IModuleContext, ReadDataContext>();
        services.AddScoped<IHumidityMeasurementContext, ReadDataContext>();

        services.AddScoped<IPlantRepository, PlantRepository>();
        services.AddScoped<IHumidityMeasurementRepository, HumidityMeasurementRepository>();
        services.AddScoped<IPlaceRepository, PlaceRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();

        services.Decorate<IPlantRepository, PlantCacheRepository>();
        services.Decorate<IHumidityMeasurementRepository, HumidityMeasurementCacheRepository>();
        services.Decorate<IPlaceRepository, PlaceCacheRepository>();
        services.Decorate<IModuleRepository, ModuleCacheRepository>();
    }

    private static string GetConnectionString()
    {
        var databaseServer = Environment.GetEnvironmentVariable("DatabaseServer");
        var databasePort = Environment.GetEnvironmentVariable("DatabasePort");
        var databaseUser = Environment.GetEnvironmentVariable("DatabaseUser");
        var databasePassword = Environment.GetEnvironmentVariable("DatabasePassword");
        var databaseName = Environment.GetEnvironmentVariable("ReadDatabaseName");

        var connectionString =
            $"Server={databaseServer},{databasePort};Database={databaseName};User Id={databaseUser};Password={databasePassword};TrustServerCertificate=true";
        return connectionString;
    }
}