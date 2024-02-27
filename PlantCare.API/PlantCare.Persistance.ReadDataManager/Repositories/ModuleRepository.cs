using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Module;
using PlantCare.Persistance.ReadDataManager.Interfaces;
using PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.ReadDataManager.Repositories;

public class ModuleRepository : IReadModuleRepository
{
    private readonly IModuleReadContext _context;
    private readonly ILogger<ModuleRepository> _logger;
    public ModuleRepository(IModuleReadContext context, ILogger<ModuleRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async ValueTask<Result<IReadOnlyCollection<IModule>>> Get()
    {
        try
        {
            var modules = await _context.Modules.ToListAsync<IModule>();
            _logger.LogInformation("Successfully loaded modules");
            return new Result<IReadOnlyCollection<IModule>>(modules);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IReadOnlyCollection<IModule>>(e);
        }
    }

    public async ValueTask<Result<IModule>> Get(Guid id)
    {
        try
        {
            var module = await _context.Modules.SingleOrDefaultAsync(m => m.Id == id);

            if (module == null)
            {
                _logger.LogError("There is no module with {Id} id", id);
                return new Result<IModule>(new NullReferenceException());
            }

            _logger.LogInformation("Successfully loaded module with {Id} id", id);
            return new Result<IModule>(module);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IModule>(e);
        }
    }
}