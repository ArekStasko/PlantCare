using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.Queries;

public static class Extensions
{
    public static void ConfigureQueries(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(QueryHandlers.PlaceQueryHandlers.PlaceQueryHandler).GetTypeInfo().Assembly));
    }
}