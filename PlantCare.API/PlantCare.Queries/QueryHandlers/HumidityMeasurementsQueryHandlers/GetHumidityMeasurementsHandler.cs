using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Queries.Abstraction.Queries.HumidityMeasurements;
using PlantCare.Queries.Abstraction.Responses.HumidityMeasurements;

namespace PlantCare.Queries.QueryHandlers.HumidityMeasurementsQueryHandlers;

public class GetHumidityMeasurementsHandler : IRequestHandler<GetHumidityMeasurementQuery, Result<List<GetHumidityMeasurementsResponse>>>
{
    private readonly IReadHumidityMeasurementRepository _repository;
    private readonly ILogger<GetHumidityMeasurementsHandler> _logger;

    public GetHumidityMeasurementsHandler(IReadHumidityMeasurementRepository repository, ILogger<GetHumidityMeasurementsHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<List<GetHumidityMeasurementsResponse>>> Handle(GetHumidityMeasurementQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetHumidityMeasurementsHandler start processing");
            var result = await _repository.Get(request.Id);
            return result.Match(succ =>
            {
                _logger.LogInformation("Successfully processed GetHumidityMeasurementsHandler query handler");

                if (request.FromDate != DateTime.MinValue || request.ToDate != DateTime.MinValue)
                {
                    succ = getSelectedPeriodOfMeasurements(succ, request.FromDate, request.ToDate);
                }

                var response = new List<GetHumidityMeasurementsResponse>();

                foreach (var measurement in succ)
                {
                    response.Add(new GetHumidityMeasurementsResponse()
                    {
                        Humidity = measurement.Humidity,
                        Date = measurement.MeasurementDate
                    });
                }

                return new Result<List<GetHumidityMeasurementsResponse>>(response);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetHumidityMeasurementsHandler request");
                return new Result<List<GetHumidityMeasurementsResponse>>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetHumidityMeasurementsHandler: {exception}", e.Message);
            return new Result<List<GetHumidityMeasurementsResponse>>(e);
        }
    }

    private IReadOnlyCollection<IHumidityMeasurement> getSelectedPeriodOfMeasurements(IReadOnlyCollection<IHumidityMeasurement> measurements, DateTime fromDate, DateTime toDate)
    {
        _logger.LogInformation("Filtering humidity measurements ...");
        IReadOnlyCollection<IHumidityMeasurement> filteredMeasurements =
            measurements.Where(hm => hm.MeasurementDate >= fromDate && hm.MeasurementDate <= toDate).ToList();

        return filteredMeasurements;
    }
}