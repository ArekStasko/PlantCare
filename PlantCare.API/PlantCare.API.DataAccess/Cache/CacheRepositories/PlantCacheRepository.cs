using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Repositories.PlantRepository;

namespace PlantCare.API.DataAccess.Cache.CacheRepositories;

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

    public async ValueTask<Result<bool>> Create(IPlant plant) => await _repository.Create(plant);


    public async ValueTask<Result<bool>> Delete(int id) => await _repository.Delete(id);

    public async ValueTask<Result<bool>> Update(IPlant plant) => await _repository.Update(plant);

    public async ValueTask<Result<IReadOnlyCollection<IPlant>>> Get()
    {
        var data = await _cache.GetRecordAsync<IReadOnlyCollection<IPlant>>("Plants");

        if (data == null)
        {
            var plants = await _repository.Get();

            plants.Match(
                succ =>
            {
                _cache.SetRecordAsync<IReadOnlyCollection<IPlant>>("Plants", succ);
                return plants;
            } ,err =>
            {
                _logger.LogError(err.Message);
                return new Result<IReadOnlyCollection<IPlant>>(err);
            });
        }

        return new Result<IReadOnlyCollection<IPlant>>(data);
    }

    public async ValueTask<Result<IPlant>> Get(int id)
    {
        
    }
}