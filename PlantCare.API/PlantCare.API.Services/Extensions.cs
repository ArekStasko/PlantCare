using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.API.Services;

public static class Extensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddMappings();
    }
    private static void AddMappings(this IServiceCollection services) => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}