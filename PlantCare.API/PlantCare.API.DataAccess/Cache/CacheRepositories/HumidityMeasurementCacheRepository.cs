using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;

namespace PlantCare.API.DataAccess.Cache.CacheRepositories;

public class HumidityMeasurementCacheRepository : IHumidityMeasurementRepository
{
    private IHumidityMeasurementRepository _repository;
    private ILogger<HumidityMeasurementCacheRepository> _logger;
    
    public HumidityMeasurementCacheRepository(IHumidityMeasurementRepository repository, ILogger<HumidityMeasurementCacheRepository> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public ValueTask<Result<bool>> Add(IHumidityMeasurement humidityMeasurement)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Result<IReadOnlyCollection<IHumidityMeasurement>>> Get(int id)
    {
        throw new NotImplementedException();
    }
}