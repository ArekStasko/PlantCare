using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlantCare.Persistance.Interfaces;
using PlantCare.Persistance.Interfaces.WriteRepositories;
using PlantCare.Persistance.WriteDataManager.Repositories;

namespace PlantCare.Persistance.WriteDataManager;

public static class Extensions
{
    public static void MigrateWriteDatabase(this IApplicationBuilder app) => DatabaseMigrationService.MigrationInitialization(app);

    public static void AddWriteDataManager(this IServiceCollection services)
    {
        services.AddWriteDataContext();
        services.AddWriteRepositories();
    }

    private static void AddWriteDataContext(this IServiceCollection services)
    {
        var connectionString = GetConnectionString();
        services.AddDbContext<WriteDataContext>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
        });
    }

    private static void AddWriteRepositories(this IServiceCollection services)
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
        var databaseServer = "192.168.1.42";
        var databasePort = "1433";
        var databaseUser = "sa";
        var databasePassword = "Password.1234";
        var databaseName = "PlantCare_Write_DB";

        var connectionString =
            $"Server={databaseServer},{databasePort};Database={databaseName};User Id={databaseUser};Password={databasePassword};TrustServerCertificate=true";
        return connectionString;
    }
}