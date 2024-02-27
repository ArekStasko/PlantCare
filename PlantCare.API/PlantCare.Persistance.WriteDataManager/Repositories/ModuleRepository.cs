using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.Models.Module;
using PlantCare.Persistance.WriteDataManager.Interfaces;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

namespace PlantCare.Persistance.WriteDataManager.Repositories;

public class ModuleRepository : IWriteModuleRepository
{
    private readonly IModuleWriteContext _context;
    private readonly ILogger<ModuleRepository> _logger;
    private readonly IDistributedCache _cache;
    public ModuleRepository(IModuleWriteContext context, ILogger<ModuleRepository> logger, IDistributedCache cache)
    {
        _context = context;
        _logger = logger;
        _cache = cache;
    }
    
    public async ValueTask<Result<Guid>> Add(Guid id)
    {
        try
        {
            var module = new Module()
            {
                Id = id
            };
            await _context.Modules.AddAsync(module);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Module with {Id} Id was successfully created", id);
            await _cache.RemoveAsync("Modules");
            _logger.LogInformation("Redis cache has been updated");
            return new Result<Guid>(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<Guid>(e);
        }
    }

    public async ValueTask<Result<bool>> Delete(Guid id)
    {
        try
        {
            var moduleToDelete = await _context.Modules.SingleOrDefaultAsync(m => m.Id == id);

            if (moduleToDelete == null)
            {
                _logger.LogError("There is no module to delete with {Id}", id);
                return new Result<bool>(new NullReferenceException());
            }

            _context.Modules.Remove(moduleToDelete);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfully deleted module with {Id} id", id);

            await _cache.RemoveAsync("Modules");
            _logger.LogInformation("Redis cache has been updated");
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }

    public async ValueTask<Result<bool>> Update(IModule module)
    {
        try
        {
            var moduleToUpdate = await _context.Modules.SingleOrDefaultAsync(m => m.Id == module.Id);

            if (moduleToUpdate == null)
            {
                _logger.LogError("There is no Module to update with {Id}", module.Id);
                return new Result<bool>(new ArgumentNullException());
            }
            
            await _context.SaveChangesAsync();

            _logger.LogInformation("Module with {Id} successfully updated", module.Id);
            await _cache.RemoveAsync($"Modules");
            _logger.LogInformation("Redis cache has been updated");
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }
}