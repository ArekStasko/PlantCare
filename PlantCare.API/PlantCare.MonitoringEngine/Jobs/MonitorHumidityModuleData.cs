using AutoMapper;
using Coravel.Invocable;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Commands.Commands.HumidityMeasurements;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Domain.Models.Module;
using HumidityMeasurement = PlantCare.Domain.Models.HumidityMeasurement.HumidityMeasurement;

namespace PlantCare.MonitoringEngine.Jobs;

public class MonitorHumidityModuleData(
    IMediator mediator,
    IReadModuleRepository moduleReadRepository,
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
            var addHumidityCommands = mapper.Map<IReadOnlyCollection<AddHumidityMeasurementCommand>>(measurements);
            var addMeasurementsResult = await AddMeasurements(addHumidityCommands);
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

    private async Task<bool> AddMeasurements(IReadOnlyCollection<AddHumidityMeasurementCommand> measurements)
    {
        bool result = false;
        List<Task<Result<bool>>> addMeasurementTasks = new List<Task<Result<bool>>>();
        foreach (var measurement in measurements) addMeasurementTasks.Add(mediator.Send(measurement));
        var addMeasurementsResult = await Task.WhenAll(addMeasurementTasks);
        return addMeasurementsResult.All(r => r.Match(succ => succ, err => throw err));
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