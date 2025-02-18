using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Module;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.ReadDataManager.CacheRepositories;

public class ModuleCacheRepository : IReadModuleRepository
{
    private readonly IReadModuleRepository _readRepository;
    private readonly ILogger<ModuleCacheRepository> _logger;
    private readonly IDistributedCache _cache;

    public ModuleCacheRepository(IReadModuleRepository readRepository, ILogger<ModuleCacheRepository> logger, IDistributedCache cache)
    {
        _readRepository = readRepository;
        _logger = logger;
        _cache = cache;
    }
    
    public async ValueTask<Result<IReadOnlyCollection<IModule>>> Get()
    {
        string modulesKey = $"Modules";
        IReadOnlyCollection<IModule> data = await _cache.GetRecordAsync<List<Module>>(modulesKey);

        if (data == null || data.Count == 0)
        {
            _logger.LogInformation("Saving Modules to cache");
            var modules = await _readRepository.Get();
            return await modules.ProcessCacheResult(_cache, modulesKey);
        }

        return new Result<IReadOnlyCollection<IModule>>(data!);
    }

    public async ValueTask<Result<IReadOnlyCollection<IModule>>> Get(int userId)
    {
        string modulesKey = $"Modules-{userId}";
        IReadOnlyCollection<IModule> data = await _cache.GetRecordAsync<List<Module>>(modulesKey);

        if (data == null || data.Count == 0)
        {
            _logger.LogInformation("Saving Modules to cache");
            var modules = await _readRepository.Get(userId);
            return await modules.ProcessCacheResult(_cache, modulesKey);
        }

        return new Result<IReadOnlyCollection<IModule>>(data!);
    }

    public async ValueTask<Result<IModule>> Get(int userId, int id)
    {
        string singleModuleKey = $"Module-{id}-{userId}";
        IModule data = await _cache.GetRecordAsync<Module>(singleModuleKey);

        if (data == null)
        {
            _logger.LogInformation("Saving module to cache");
            var module = await _readRepository.Get(userId, id);
            return await module.ProcessCacheResult(_cache, singleModuleKey);
        }

        return new Result<IModule>(data);
    }
}