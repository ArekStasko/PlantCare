using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Dto;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;
using PlantCare.Queries.Queries.HumidityMeasurements;
using PlantCare.Queries.Responses.HumidityMeasurements;

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
        
    }

}