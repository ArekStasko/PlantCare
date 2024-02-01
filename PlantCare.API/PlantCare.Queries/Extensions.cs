using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.Queries;

public static class Extensions
{
    public static void ConfigureQueries(this IServiceCollection services)
    {
        
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(
                typeof(QueryHandlers.PlaceQueryHandlers.PlaceQueryHandler).GetTypeInfo().Assembly,
                typeof(QueryHandlers.ModuleQueryHandlers.GetModulesHandler).GetTypeInfo().Assembly,
                typeof(QueryHandlers.PlantQueryHandlers.GetPlantsHandler).GetTypeInfo().Assembly,
                typeof(QueryHandlers.HumidityMeasurementsQueryHandlers.GetHumidityMeasurementsHandler).GetTypeInfo().Assembly
            ));
    }
    
    public static void AddQueriesMapperProfile(this IServiceCollection services) => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}