using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;
using PlantCare.API.Services.Queries.HumidityMeasurementsQueries;

namespace PlantCare.API.Services.QueryHandlers.HumidityMeasurementsQueryHandlers;

public class GetHumidityMeasurementsHandler : IRequestHandler<GetHumidityMeasurementQuery, Result<IReadOnlyCollection<IHumidityMeasurement>>>
{
    private readonly IReadHumidityMeasurementRepository _repository;
    private readonly ILogger<GetHumidityMeasurementsHandler> _logger;

    public GetHumidityMeasurementsHandler(IReadHumidityMeasurementRepository repository, ILogger<GetHumidityMeasurementsHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyCollection<IHumidityMeasurement>>> Handle(GetHumidityMeasurementQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("GetHumidityMeasurementsHandler start processing");
            var result = await _repository.Get(request.Id);
            return result.Match(succ =>
            {
                _logger.LogInformation("Successfully processed GetHumidityMeasurementsHandler query handler");

                if (request.FromDate != null || request.ToDate != null)
                {
                    succ = getSelectedPeriodOfMeasurements(succ, request.FromDate, request.ToDate);
                }

                return new Result<IReadOnlyCollection<IHumidityMeasurement>>(succ);
            }, err =>
            {
                _logger.LogError("Something went wrong while processing GetHumidityMeasurementsHandler request");
                return new Result<IReadOnlyCollection<IHumidityMeasurement>>(err);
            });
        }
        catch (Exception e)
        {
            _logger.LogError("Exception has been thrown in GetHumidityMeasurementsHandler: {exception}", e.Message);
            return new Result<IReadOnlyCollection<IHumidityMeasurement>>(e);
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