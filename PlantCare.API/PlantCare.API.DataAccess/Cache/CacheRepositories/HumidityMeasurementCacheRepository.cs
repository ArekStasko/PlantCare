namespace PlantCare.API.DataAccess.Cache.CacheRepositories;

using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;

public class HumidityMeasurementCacheRepository : IReadHumidityMeasurementRepository
{
    private IReadHumidityMeasurementRepository _repository;
    private ILogger<HumidityMeasurementCacheRepository> _logger;

    public HumidityMeasurementCacheRepository(IReadHumidityMeasurementRepository repository, ILogger<HumidityMeasurementCacheRepository> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public ValueTask<Result<IReadOnlyCollection<IHumidityMeasurement>>> Get(int id)
    {
        throw new NotImplementedException();
    }
}