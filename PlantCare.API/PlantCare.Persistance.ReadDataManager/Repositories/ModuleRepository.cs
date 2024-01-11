using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Module;
using PlantCare.Persistance.DAO.Module;
using PlantCare.Persistance.Interfaces;
using PlantCare.Persistance.Interfaces.ReadRepositories;

namespace PlantCare.Persistance.ReadDataManager.Repositories;

public class ModuleRepository : IReadModuleRepository
{
    private readonly IModuleContext _context;
    private readonly ILogger<ModuleRepository> _logger;
    public ModuleRepository(IModuleContext context, ILogger<ModuleRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async ValueTask<Result<IReadOnlyCollection<IModuleDAO>>> Get()
    {
        try
        {
            var modules = await _context.Modules.ToListAsync<IModuleDAO>();
            _logger.LogInformation("Successfully loaded modules");
            return new Result<IReadOnlyCollection<IModuleDAO>>(modules);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IReadOnlyCollection<IModuleDAO>>(e);
        }
    }

    public async ValueTask<Result<IModuleDAO>> Get(Guid id)
    {
        try
        {
            var module = await _context.Modules.SingleOrDefaultAsync(m => m.Id == id);

            if (module == null)
            {
                _logger.LogError("There is no module with {Id} id", id);
                return new Result<IModuleDAO>(new NullReferenceException());
            }

            _logger.LogInformation("Successfully loaded module with {Id} id", id);
            return new Result<IModuleDAO>(module);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IModuleDAO>(e);
        }
    }
}