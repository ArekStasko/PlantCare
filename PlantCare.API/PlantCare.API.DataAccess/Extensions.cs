using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlantCare.API.DataAccess.Cache.CacheRepositories;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;
using PlantCare.API.DataAccess.Repositories.ModuleRepository;
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

    public static void SetupCache(this IServiceCollection services)
    {
        var redisConnectionString = $"192.168.1.40:6379,password=eYVX7EwVmmxKPCDmwMtyKVge8oLd2t81";
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
        services.AddDbContext<DataContext>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPlaceContext, DataContext>();
        services.AddScoped<IPlantContext, DataContext>();
        services.AddScoped<IModuleContext, DataContext>();
        services.AddScoped<IHumidityMeasurementContext, DataContext>();

        services.AddScoped<IWritePlantRepository, PlantRepository>();
        services.AddScoped<IWritePlaceRepository, PlaceRepository>();
        services.AddScoped<IWriteModuleRepository, ModuleRepository>();
        services.AddScoped<IWriteHumidityMeasurementRepository, HumidityMeasurementRepository>();

        services.AddScoped<IReadPlantRepository, PlantRepository>();
        services.AddScoped<IReadHumidityMeasurementRepository, HumidityMeasurementRepository>();
        services.AddScoped<IReadPlaceRepository, PlaceRepository>();
        services.AddScoped<IReadModuleRepository, ModuleRepository>();

        services.Decorate<IReadPlantRepository, PlantCacheRepository>();
        services.Decorate<IReadHumidityMeasurementRepository, HumidityMeasurementCacheRepository>();
        services.Decorate<IReadPlaceRepository, PlaceCacheRepository>();
        services.Decorate<IReadModuleRepository, ModuleCacheRepository>();
    }

    private static string GetConnectionString()
    {
        var databaseServer = Environment.GetEnvironmentVariable("DatabaseServer");
        var databasePort = Environment.GetEnvironmentVariable("DatabasePort");
        var databaseUser = Environment.GetEnvironmentVariable("DatabaseUser");
        var databasePassword = Environment.GetEnvironmentVariable("DatabasePassword");
        var databaseName = Environment.GetEnvironmentVariable("DatabaseName");

        var connectionString =
            $"Server=192.168.1.40,1433;Database=PlantCare_DB;User Id=sa;Password=Password.1234;TrustServerCertificate=true";
        return connectionString;
    }
}