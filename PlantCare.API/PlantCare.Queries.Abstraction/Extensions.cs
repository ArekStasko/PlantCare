using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.Queries.Abstraction;

public static class Extensions
{
    
    public static void ConfigureQueries(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(Queries.Plant.GetPlantQuery).GetTypeInfo().Assembly));
    }

}