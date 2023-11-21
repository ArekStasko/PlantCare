using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.Place;
using PlantCare.API.DataAccess.Repositories.PlaceRepository;

namespace PlantCare.API.DataAccess.Cache.CacheRepositories;

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

    public async ValueTask<Result<IReadOnlyCollection<IPlace>>> Get()
    {
        string placesKey = "Places";
        var data = await _cache.GetRecordAsync<IReadOnlyCollection<IPlace>>(placesKey);
        
        if (data.Count == 0)
        {
            _logger.LogInformation("Saving place to cache");
            var places = await _repository.Get();
            return await places.ProcessCacheResult(_cache, placesKey);
        }

        return new Result<IReadOnlyCollection<IPlace>>(data!);
    }
}