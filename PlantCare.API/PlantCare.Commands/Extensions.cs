using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.Commands;

public static class Extensions
{
    public static void ConfigureCommands(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(typeof(CommandHandlers.PlantCommandHandlers.CreatePlantHandler).GetTypeInfo().Assembly));
    }
}