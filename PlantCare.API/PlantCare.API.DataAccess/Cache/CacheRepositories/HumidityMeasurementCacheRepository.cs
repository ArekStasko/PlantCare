using Microsoft.Extensions.Caching.Distributed;

namespace PlantCare.API.DataAccess.Cache.CacheRepositories;

using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;

public class HumidityMeasurementCacheRepository : IReadHumidityMeasurementRepository
{
    private IReadHumidityMeasurementRepository _repository;
    private ILogger<HumidityMeasurementCacheRepository> _logger;
    private readonly IDistributedCache _cache;
    
    public HumidityMeasurementCacheRepository(IReadHumidityMeasurementRepository repository, ILogger<HumidityMeasurementCacheRepository> logger, IDistributedCache cache)
    {
        _repository = repository;
        _logger = logger;
        _cache = cache;
    }


    public async ValueTask<Result<IReadOnlyCollection<IHumidityMeasurement>>> Get(int id)
    {
        string humidityMeasurementsKey = $"HumidityMeasurements-{id}";
        IReadOnlyCollection<IHumidityMeasurement> data = await _cache.GetRecordAsync<List<HumidityMeasurement>>(humidityMeasurementsKey);

        if (data == null || data.Count == 0)
        {
            _logger.LogInformation("Saving Humidity Measurements to cache");
            var humidityMeasurement = await _repository.Get(id);
            return await humidityMeasurement.ProcessCacheResult(_cache, humidityMeasurementsKey);
        }

        return new Result<IReadOnlyCollection<IHumidityMeasurement>>(data!);
    }

}