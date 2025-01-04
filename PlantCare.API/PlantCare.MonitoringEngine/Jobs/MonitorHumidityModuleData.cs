using Coravel.Invocable;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Domain.Models.Module;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.MonitoringEngine.Jobs;

public class MonitorHumidityModuleData(
    HttpClient httpClient,
    IReadModuleRepository moduleReadRepository,
    IWriteHumidityMeasurementRepository humidityWriteRepository,
    ILogger<MonitorHumidityModuleData> logger
    ) : IInvocable
{
    
    public async Task Invoke()
    {
        try
        {
            var result = await moduleReadRepository.Get();
            var modules = result.Match<IReadOnlyCollection<IModule>>(succ =>
                succ.Where(s => s.IsMonitoring).ToList()
            , error =>
            {
                logger.LogError(error.Message);
                throw error;
            });
            
            if (!modules.Any()) return;

            List<Task<IHumidityMeasurement>> getMeasurementsTasks = new List<Task<IHumidityMeasurement>>();
            foreach (var module in modules) getMeasurementsTasks.Add(GetHumidity(module));
            var measurements = await Task.WhenAll(getMeasurementsTasks);
            foreach (var measurement in measurements) await humidityWriteRepository.Add(measurement);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<IHumidityMeasurement> GetHumidity(IModule module)
    {
        string uri = $"UriToModule";
        HttpResponseMessage response = await httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}