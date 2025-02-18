using Coravel;
using Microsoft.Extensions.DependencyInjection;
using PlantCare.MonitoringEngine.Jobs;

namespace PlantCare.MonitoringEngine;

public static class Extentions
{
    public static void AddJobs(this IServiceCollection services)
    {
        services.AddScheduler();
        services.AddTransient<MonitorHumidityModuleData>();
    }
}