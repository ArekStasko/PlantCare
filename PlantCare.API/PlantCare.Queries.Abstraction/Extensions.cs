using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.Queries.Abstraction;

public static class Extensions
{
    public static void AddQueriesMapperProfile(this IServiceCollection services) => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}