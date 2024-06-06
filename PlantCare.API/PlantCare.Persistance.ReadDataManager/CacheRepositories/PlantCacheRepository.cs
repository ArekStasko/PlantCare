using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Plant;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.ReadDataManager.CacheRepositories;

public class PlantCacheRepository : IReadPlantRepository
{
    private readonly IReadPlantRepository _repository;
    private readonly IDistributedCache _cache;
    private readonly ILogger<PlantCacheRepository> _logger;

    public PlantCacheRepository(IReadPlantRepository repository, IDistributedCache cache, ILogger<PlantCacheRepository> logger)
    {
        _repository = repository;
        _cache = cache;
        _logger = logger;
    }

    public async ValueTask<Result<IReadOnlyCollection<IPlant>>> Get(int userId)
    {
        string plantsKey = $"Plants-{userId}";
        IReadOnlyCollection<IPlant> data = await _cache.GetRecordAsync<List<Plant>>(plantsKey);

        if (data == null || data.Count == 0)
        {
            _logger.LogInformation("Saving plants to cache");
            var plants = await _repository.Get(userId);
            return await plants.ProcessCacheResult(_cache, plantsKey);
        }

        return new Result<IReadOnlyCollection<IPlant>>(data!);
    }

    public async ValueTask<Result<IPlant>> Get(int id, int userId)
    {
        string singlePlantKey = $"Plant-{id}-{userId}";
        IPlant data = await _cache.GetRecordAsync<Plant>(singlePlantKey);

        if (data == null)
        {
            _logger.LogInformation("Saving plant to cache");
            var plant = await _repository.Get(id, userId);
            return await plant.ProcessCacheResult(_cache, singlePlantKey);
        }

        return new Result<IPlant>(data);
    }
}