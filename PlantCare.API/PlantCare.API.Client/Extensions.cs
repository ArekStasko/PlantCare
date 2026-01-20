using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.API.Client;

public static class Extensions
{
    public static void ConfigureClient(this IServiceCollection services)
    {
        services.AddOpenApiDocument();
    }
}