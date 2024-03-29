﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlantCare.Domain.CommonContexts.ConsistencyManagerContexts;
using PlantCare.Persistance.ReadDataManager.CacheRepositories;
using PlantCare.Persistance.ReadDataManager.Interfaces;
using PlantCare.Persistance.ReadDataManager.Repositories;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.ReadDataManager;

public static class Extensions
{
    public static void MigrateReadDatabase(this IApplicationBuilder app) => DatabaseMigrationService.MigrationInitialization(app);

    public static void AddReadDataManager(this IServiceCollection services)
    {
        services.AddReadDataContext();
        services.AddReadRepositories();
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
        services.AddScoped<IPlaceReadContext, ReadDataContext>();
        services.AddScoped<IPlantReadContext, ReadDataContext>();
        services.AddScoped<IModuleReadContext, ReadDataContext>();
        services.AddScoped<IHumidityMeasurementReadContext, ReadDataContext>();

        services.AddScoped<IHumidityMeasurementsConsistencyContext, ReadDataContext>();
        services.AddScoped<IPlaceConsistencyContext, ReadDataContext>();
        services.AddScoped<IPlantConsistencyContext, ReadDataContext>();
        services.AddScoped<IModuleConsistencyContext, ReadDataContext>();
        
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
        var databaseName = Environment.GetEnvironmentVariable("ReadDatabaseName");

        var connectionString =
            $"Server={databaseServer},{databasePort};Database={databaseName};User Id={databaseUser};Password={databasePassword};TrustServerCertificate=true";
        return connectionString;
    }
}