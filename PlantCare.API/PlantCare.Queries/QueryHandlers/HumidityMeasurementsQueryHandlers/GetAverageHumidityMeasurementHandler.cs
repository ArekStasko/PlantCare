using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Dto;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.HumidityMeasurements;

namespace PlantCare.Queries.QueryHandlers.HumidityMeasurementsQueryHandlers;

public class GetAverageHumidityMeasurementHandler : IRequestHandler<GetAverageHumidityMeasurementQuery, Result<List<AverageHumidity>>>
{
    private readonly IReadHumidityMeasurementRepository _repository;
    private readonly ILogger<GetHumidityMeasurementsHandler> _logger;

    public GetAverageHumidityMeasurementHandler(IReadHumidityMeasurementRepository repository, ILogger<GetHumidityMeasurementsHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<AverageHumidity>>> Handle(GetAverageHumidityMeasurementQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var humidityMeasurements = (await _repository.Get(request.ModuleId)).Match(measurements => measurements,
                err =>
                {
                    _logger.LogError("Error occured while getting average humidity measurements: {err}", err);
                    throw err;
                });

            var filteredHumidityMeasurements = getSelectedPeriodOfMeasurements(humidityMeasurements, request.FromDate, request.ToDate);
            IDictionary<string, List<int>> humidityMeasurementsByDate = new Dictionary<string, List<int>>();
            foreach (var humidityMeasurement in filteredHumidityMeasurements)
            {
                if (!humidityMeasurementsByDate.ContainsKey(
                        humidityMeasurement.MeasurementDate.Date.ToShortDateString()))
                {
                    humidityMeasurementsByDate.Add(humidityMeasurement.MeasurementDate.Date.ToShortDateString(), [humidityMeasurement.Humidity]);
                    continue;
                }

                humidityMeasurementsByDate[humidityMeasurement.MeasurementDate.Date.ToShortDateString()].Add(humidityMeasurement.Humidity);
            }
            
            var result = new List<AverageHumidity>();
            
            foreach (var key in humidityMeasurementsByDate.Keys)
            {
                var humidityMeasurementsByDay = humidityMeasurementsByDate[key];
                result.Add(new AverageHumidity()
                {
                    Date = key,
                    Humidity = (int)humidityMeasurementsByDay.Average()
                });
            }

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError("Something went wrong while running GetAverageHumidityMeasurement Query: {e}", e);
            return new Result<List<AverageHumidity>>(e);
        }
    }

    private IReadOnlyCollection<IHumidityMeasurement> getSelectedPeriodOfMeasurements(IReadOnlyCollection<IHumidityMeasurement> measurements, DateTime fromDate, DateTime toDate)
    {
        IReadOnlyCollection<IHumidityMeasurement> filteredMeasurements =
            measurements.Where(hm => hm.MeasurementDate >= fromDate && hm.MeasurementDate <= toDate).ToList();

        return filteredMeasurements;
    }
}