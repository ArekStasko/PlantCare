using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.Commands.Abstraction;

public static class Extentions
{
    public static void AddCommandsMapperProfile(this IServiceCollection services) => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
}