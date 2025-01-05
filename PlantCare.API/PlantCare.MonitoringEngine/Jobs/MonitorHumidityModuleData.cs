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
            logger.LogInformation("Check humidity measurements");
            var result = await moduleReadRepository.Get();
            var modules = result.Match<IReadOnlyCollection<IModule>>(succ =>
                succ.Where(s => s.IsMonitoring).ToList()
            , error =>
            {
                logger.LogError(error.Message);
                throw error;
            });

            if (!modules.Any())
            {
                logger.LogInformation("There are no modules to check");
                return;
            };
            var measurements = await GetHumidity(modules);
            var addMeasurementsResult = await AddMeasurements(measurements);
            if (!addMeasurementsResult) logger.LogError($"Failed to add humidity measurements");
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
    }

    private async Task<IHumidityMeasurement[]> GetHumidity(IReadOnlyCollection<IModule> modules)
    {
        List<Task<IHumidityMeasurement>> getMeasurementsTasks = new List<Task<IHumidityMeasurement>>();
        foreach (var module in modules) getMeasurementsTasks.Add(GetHumidity(module));
        return await Task.WhenAll(getMeasurementsTasks);
    }

    private async Task<bool> AddMeasurements(IHumidityMeasurement[] measurements)
    {
        bool result = false;
        List<Task<Result<int>>> addMeasurementTasks = new List<Task<Result<int>>>();
        foreach (var measurement in measurements) addMeasurementTasks.Add(humidityWriteRepository.Add(measurement));
        var addMeasurementsResult = await Task.WhenAll(addMeasurementTasks);
        foreach (var addMeasurement in addMeasurementsResult)
        {
          result = addMeasurement.Match(succ => true, err => false);
          if (!result) break;
        }
        return result;
    }

    private async Task<IHumidityMeasurement> GetHumidity(IModule module)
    {
        string uri = $"UriToModule";
        HttpResponseMessage response = await httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var humidity = await response.Content.ReadAsStringAsync();
        return new HumidityMeasurement()
        {
            ModuleId = module.Id,
            MeasurementDate = DateTime.UtcNow,
            Humidity = Int32.Parse(humidity),
        };
    }
}