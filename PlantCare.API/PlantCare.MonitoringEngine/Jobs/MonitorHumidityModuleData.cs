using AutoMapper;
using Coravel.Invocable;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Dto;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Domain.Models.Module;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;
using HumidityMeasurement = PlantCare.Domain.Models.HumidityMeasurement.HumidityMeasurement;
using HumidityMeasurementMessage = PlantCare.MessageBroker.Messages.HumidityMeasurement;

namespace PlantCare.MonitoringEngine.Jobs;

public class MonitorHumidityModuleData(
    IReadModuleRepository moduleReadRepository,
    IWriteHumidityMeasurementRepository humidityWriteRepository,
    QueueProducer<HumidityMeasurementMessage> producer,
    ILogger<MonitorHumidityModuleData> logger,
    IMapper mapper
    ) : IInvocable
{
    private string ModuleUrl = Environment.GetEnvironmentVariable("ModuleUrl"); 
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
          result = addMeasurement.Match(succ =>
          {
              var humidityMeasurementDto = mapper.Map<HumidityMeasurementDto>(measurements.FirstOrDefault(m => m.Id == succ));
              humidityMeasurementDto.Id = succ;
              var humidityMeasurementMessage = new HumidityMeasurementMessage()
              {
                  Action = ActionType.Add,
                  HumidityMeasurementData = humidityMeasurementDto
              };
              producer.PublishMessage(humidityMeasurementMessage);
                    
              logger.LogInformation("Successfully added humidity measurement");
              return true;
          }, err => false);
          if (!result) break;
        }
        return result;
    }

    private async Task<IHumidityMeasurement> GetHumidity(IModule module)
    {
        if (ModuleUrl == null)
            throw new NullReferenceException("HumidityRoute is null");

        using HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(ModuleUrl);
        using HttpResponseMessage response = await httpClient.GetAsync("/humidity?id=" + module.Id);
        var humidity = await response.Content.ReadAsStringAsync();
        logger.LogInformation("humidity: {humidity}", humidity);
        response.EnsureSuccessStatusCode();
                
        return new HumidityMeasurement()
        {
            ModuleId = module.Id,
            MeasurementDate = DateTime.UtcNow,
            Humidity = Int32.Parse(humidity),
        };
    }
}