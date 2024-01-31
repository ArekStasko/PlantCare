using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlantCare.Persistance.Interfaces;
using PlantCare.Persistance.Interfaces.ReadRepositories;
using PlantCare.Persistance.ReadDataManager.CacheRepositories;
using PlantCare.Persistance.ReadDataManager.Repositories;

namespace PlantCare.Persistance.ReadDataManager;

public static class Extensions
{
    public static void MigrateReadDatabase(this IApplicationBuilder app) => DatabaseMigrationService.MigrationInitialization(app);

    public static void AddReadDataManager(this IServiceCollection services)
    {
        services.AddReadDataContext();
        services.AddReadRepositories();
    }

    public static void AddReadCache(this IServiceCollection services)
    {
        var redisConnectionString = $"http://192.168.1.42:6379,password=eYVX7EwVmmxKPCDmwMtyKVge8oLd2t81";
        var redisInstance = "plantcare";

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = redisInstance;
        });
    }

    private static void AddReadDataContext(this IServiceCollection services)
    {
        var connectionString = GetConnectionString();
        services.AddDbContext<ReadDataContext>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
        });
    }

    private static void AddReadRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPlaceContext, ReadDataContext>();
        services.AddScoped<IPlantContext, ReadDataContext>();
        services.AddScoped<IModuleContext, ReadDataContext>();
        services.AddScoped<IHumidityMeasurementContext, ReadDataContext>();

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
        var databaseServer = "192.168.1.42";
        var databasePort = "1433";
        var databaseUser = "sa";
        var databasePassword = "Password.1234";
        var databaseName = "PlantCare_Read_DB";

        var connectionString =
            $"Server={databaseServer},{databasePort};Database={databaseName};User Id={databaseUser};Password={databasePassword};TrustServerCertificate=true";
        return connectionString;
    }
}