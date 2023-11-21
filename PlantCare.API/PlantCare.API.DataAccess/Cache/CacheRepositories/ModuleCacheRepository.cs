using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.DataAccess.Repositories.ModuleRepository;

namespace PlantCare.API.DataAccess.Cache.CacheRepositories;

public class ModuleCacheRepository : IReadModuleRepository
{
    private readonly IReadModuleRepository _repository;
    private readonly ILogger<ModuleCacheRepository> _logger;
    private readonly IDistributedCache _cache;
    
    public ModuleCacheRepository(IReadModuleRepository repository, ILogger<ModuleCacheRepository> logger, IDistributedCache cache)
    {
        _repository = repository;
        _logger = logger;
        _cache = cache;
    }
    
    public async ValueTask<Result<IReadOnlyCollection<IModule>>> Get()
    {
        string modulesKey = "Modules";
        var data = await _cache.GetRecordAsync<IReadOnlyCollection<IModule>>(modulesKey);

        if (data.Count == 0)
        {
            _logger.LogInformation("Saving Modules to cache");
            var modules = await _repository.Get();
            return await modules.ProcessCacheResult(_cache, modulesKey);
        }

        return new Result<IReadOnlyCollection<IModule>>(data!);
    }

    public async ValueTask<Result<IModule>> Get(int id)
    {
        string singleModuleKey = $"Module-{id}";
        var data = await _cache.GetRecordAsync<IModule>(singleModuleKey);

        if (data == null)
        {
            _logger.LogInformation("Saving module to cache");
            var module = await _repository.Get(id);
            return await module.ProcessCacheResult(_cache, singleModuleKey);
        }

        return new Result<IModule>(data);
    }
}