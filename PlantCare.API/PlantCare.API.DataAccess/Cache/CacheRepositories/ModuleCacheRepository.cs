using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.DataAccess.Repositories.ModuleRepository;

namespace PlantCare.API.DataAccess.Cache.CacheRepositories;

public class ModuleCacheRepository : IReadModuleRepository
{
    private readonly IReadModuleRepository _repository;
    private readonly ILogger<ModuleCacheRepository> _logger;
    
    public ModuleCacheRepository(IReadModuleRepository repository, ILogger<ModuleCacheRepository> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public ValueTask<Result<IReadOnlyCollection<IModule>>> Get()
    {
        throw new NotImplementedException();
    }

    public ValueTask<Result<IModule>> Get(int id)
    {
        throw new NotImplementedException();
    }
}