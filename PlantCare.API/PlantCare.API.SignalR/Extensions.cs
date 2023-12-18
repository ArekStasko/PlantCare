using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.API.SignalR;

public static class Extensions
{
    public static void ConfigureSignalRService(this IServiceCollection services)
    {
        services.AddSignalR();
    }
}
