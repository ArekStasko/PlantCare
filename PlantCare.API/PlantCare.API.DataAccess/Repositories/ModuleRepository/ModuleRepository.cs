namespace PlantCare.API.DataAccess.Repositories.ModuleRepository;

using AutoMapper;
using Castle.Core.Logging;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models.Module;

public class ModuleRepository : IWriteModuleRepository, IReadModuleRepository
{
    private readonly IModuleContext _context;
    private readonly ILogger<ModuleRepository> _logger;
    private readonly IDistributedCache _cache;
    public ModuleRepository(IModuleContext context, ILogger<ModuleRepository> logger, IDistributedCache cache)
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

            moduleToUpdate.RequiredMoistureLevel = module.RequiredMoistureLevel;
            moduleToUpdate.CriticalMoistureLevel = module.CriticalMoistureLevel;
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