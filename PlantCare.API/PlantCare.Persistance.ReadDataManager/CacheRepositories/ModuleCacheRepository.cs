using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Persistance.DAO.Module;
using PlantCare.Persistance.ReadDataManager.Repositories.ModelRepository;

namespace PlantCare.Persistance.ReadDataManager.CacheRepositories;

public class ModuleCacheRepository : IModelRepository
{
    private readonly IModelRepository _readRepository;
    private readonly ILogger<ModuleCacheRepository> _logger;
    private readonly IDistributedCache _cache;

    public ModuleCacheRepository(IModelRepository readRepository, ILogger<ModuleCacheRepository> logger, IDistributedCache cache)
    {
        _readRepository = readRepository;
        _logger = logger;
        _cache = cache;
    }

    public async ValueTask<Result<IReadOnlyCollection<IModuleDAO>>> Get()
    {
        string modulesKey = "Modules";
        IReadOnlyCollection<IModuleDAO> data = await _cache.GetRecordAsync<List<IModuleDAO>>(modulesKey);

        if (data == null || data.Count == 0)
        {
            _logger.LogInformation("Saving Modules to cache");
            var modules = await _readRepository.Get();
            return await modules.ProcessCacheResult(_cache, modulesKey);
        }

        return new Result<IReadOnlyCollection<IModuleDAO>>(data!);
    }

    public async ValueTask<Result<IModuleDAO>> Get(Guid id)
    {
        string singleModuleKey = $"Module-{id}";
        IModuleDAO data = await _cache.GetRecordAsync<IModuleDAO>(singleModuleKey);

        if (data == null)
        {
            _logger.LogInformation("Saving module to cache");
            var module = await _readRepository.Get(id);
            return await module.ProcessCacheResult(_cache, singleModuleKey);
        }

        return new Result<IModuleDAO>(data);
    }
}