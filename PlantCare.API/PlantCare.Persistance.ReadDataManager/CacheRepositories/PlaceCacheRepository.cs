using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.DAO.Place;
using PlantCare.Persistance.ReadDataManager.Repositories.PlaceRepository;

namespace PlantCare.Persistance.ReadDataManager.CacheRepositories;

public class PlaceCacheRepository : IPlaceRepository
{
    private readonly IPlaceRepository _repository;
    private readonly ILogger<PlaceCacheRepository> _logger;
    private readonly IDistributedCache _cache;
    public PlaceCacheRepository(IPlaceRepository repository, ILogger<PlaceCacheRepository> logger, IDistributedCache cache)
    {
        _repository = repository;
        _logger = logger;
        _cache = cache;
    }

    public async ValueTask<Result<IReadOnlyCollection<IPlaceDAO>>> Get()
    {
        string placesKey = "Places";
        IReadOnlyCollection<IPlaceDAO> data = await _cache.GetRecordAsync<List<IPlaceDAO>>(placesKey);

        if (data == null || data.Count == 0)
        {
            _logger.LogInformation("Saving place to cache");
            var places = await _repository.Get();
            return await places.ProcessCacheResult(_cache, placesKey);
        }

        return new Result<IReadOnlyCollection<IPlaceDAO>>(data!);
    }
}