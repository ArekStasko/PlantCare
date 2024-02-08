using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.ConsistencyManager;

public static class Extensions
{
    public static void AddConsistencyManagerMapperProfile(this IServiceCollection services) => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}