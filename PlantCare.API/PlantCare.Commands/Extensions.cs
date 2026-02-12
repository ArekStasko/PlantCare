using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.Commands;

public static class Extensions
{
    public static void ConfigureCommands(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(
                typeof(CommandHandlers.PlantCommandHandlers.CreatePlantHandler).GetTypeInfo().Assembly,
                typeof(CommandHandlers.PlaceCommandHandlers.CreatePlaceHandler).GetTypeInfo().Assembly,
                typeof(CommandHandlers.ModuleCommandHandlers.CreateModuleHandler).GetTypeInfo().Assembly,
                typeof(CommandHandlers.HumidityMeasurementCommandHandlers.AddHumidityMeasurementHandler).GetTypeInfo().Assembly
                ));
    }
    
    public static void AddCommandsMapperProfile(this IServiceCollection services) => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

}