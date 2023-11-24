using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.Place;
using PlantCare.API.DataAccess.Repositories.PlaceRepository;

namespace PlantCare.API.DataAccess.Cache.CacheRepositories;

public class PlaceCacheRepository : IReadPlaceRepository
{
    private readonly IReadPlaceRepository _readRepository;
    private readonly ILogger<PlaceCacheRepository> _logger;
    private readonly IDistributedCache _cache;
    public PlaceCacheRepository(IReadPlaceRepository readRepository, ILogger<PlaceCacheRepository> logger, IDistributedCache cache)
    {
        _readRepository = readRepository;
        _logger = logger;
        _cache = cache;
    }

    public async ValueTask<Result<IReadOnlyCollection<IPlace>>> Get()
    {
        _logger.LogInformation("Waiting for redis...");
        string placesKey = "Places";
        IReadOnlyCollection<IPlace> data = await _cache.GetRecordAsync<List<Place>>(placesKey);
        _logger.LogInformation("Redis fine go next");
        
        if (data == null || data.Count == 0)
        {
            _logger.LogInformation("Saving place to cache");
            var places = await _readRepository.Get();
            return await places.ProcessCacheResult(_cache, placesKey);
        }

        return new Result<IReadOnlyCollection<IPlace>>(data!);
    }
}