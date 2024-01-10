using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.DAO.Plant;
using PlantCare.Persistance.ReadDataManager.Repositories.PlantRepository;

namespace PlantCare.Persistance.ReadDataManager.CacheRepositories;

public class PlantCacheRepository : IPlantRepository
{
    private readonly IPlantRepository _repository;
    private readonly IDistributedCache _cache;
    private readonly ILogger<PlantCacheRepository> _logger;

    public PlantCacheRepository(IPlantRepository repository, IDistributedCache cache, ILogger<PlantCacheRepository> logger)
    {
        _repository = repository;
        _cache = cache;
        _logger = logger;
    }

    public async ValueTask<Result<IReadOnlyCollection<IPlantDAO>>> Get()
    {
        string plantsKey = "Plants";
        IReadOnlyCollection<IPlantDAO> data = await _cache.GetRecordAsync<List<IPlantDAO>>(plantsKey);

        if (data == null || data.Count == 0)
        {
            _logger.LogInformation("Saving plants to cache");
            var plants = await _repository.Get();
            return await plants.ProcessCacheResult(_cache, plantsKey);
        }

        return new Result<IReadOnlyCollection<IPlantDAO>>(data!);
    }

    public async ValueTask<Result<IPlantDAO>> Get(int id)
    {
        string singlePlantKey = $"Plant-{id}";
        IPlantDAO data = await _cache.GetRecordAsync<IPlantDAO>(singlePlantKey);

        if (data == null)
        {
            _logger.LogInformation("Saving plant to cache");
            var plant = await _repository.Get(id);
            return await plant.ProcessCacheResult(_cache, singlePlantKey);
        }

        return new Result<IPlantDAO>(data);
    }
}