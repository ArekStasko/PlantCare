using Microsoft.Extensions.DependencyInjection;
using PlantCare.ConsistencyManager.Services;

namespace PlantCare.ConsistencyManager;

public static class Extensions
{
    public static void AddConsistencyManager(this IServiceCollection services) =>
        services.AddScoped<IConsistencyService, ConsistencyService>();
    public static void AddConsistencyManagerMapperProfile(this IServiceCollection services) => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}