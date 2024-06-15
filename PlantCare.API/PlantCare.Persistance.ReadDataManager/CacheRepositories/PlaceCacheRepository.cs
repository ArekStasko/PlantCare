using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Place;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.ReadDataManager.CacheRepositories;

public class PlaceCacheRepository : IReadPlaceRepository
{
    private readonly IReadPlaceRepository _repository;
    private readonly ILogger<PlaceCacheRepository> _logger;
    private readonly IDistributedCache _cache;
    public PlaceCacheRepository(IReadPlaceRepository repository, ILogger<PlaceCacheRepository> logger, IDistributedCache cache)
    {
        _repository = repository;
        _logger = logger;
        _cache = cache;
    }

    public async ValueTask<Result<IReadOnlyCollection<IPlace>>> Get(int userId)
    {
        string placesKey = $"Places-{userId}";
        IReadOnlyCollection<IPlace> data = await _cache.GetRecordAsync<List<Place>>(placesKey);
        
        if (data == null || data.Count == 0)
        {
            _logger.LogInformation("Saving place to cache");
            var places = await _repository.Get(userId);
            return await places.ProcessCacheResult(_cache, placesKey);
        }

        return new Result<IReadOnlyCollection<IPlace>>(data!);
    }
}