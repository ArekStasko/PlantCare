using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.ReadDataManager.CacheRepositories;

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

    public async ValueTask<Result<int>> GetLatest(int id)
    {
        string humidityMeasurementLatestKey = $"HumidityMeasurementLatest-{id}";
        int data = await _cache.GetRecordAsync<int>(humidityMeasurementLatestKey);
        
        if (data == 0)
        {
            _logger.LogInformation("Saving Latest Humidity Measurement to cache");
            var humidityMeasurement = await _repository.GetLatest(id);
            return await humidityMeasurement.ProcessCacheResult(_cache, humidityMeasurementLatestKey);
        }

        return new Result<int>(data!);
    }
}