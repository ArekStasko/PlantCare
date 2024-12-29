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
    public ModuleRepository(IModuleWriteContext context, ILogger<ModuleRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async ValueTask<Result<int>> Add(int userId)
    {
        try
        {
            var module = new Module()
            {
                UserId = userId
            };
            await _context.Modules.AddAsync(module);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Module with {Id} Id was successfully created", module.Id);
            return new Result<int>(module.Id);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<int>(e);
        }
    }

    public async ValueTask<Result<bool>> Delete(int userId, int id)
    {
        try
        {
            var moduleToDelete = await _context.Modules.SingleOrDefaultAsync(m => m.Id == id && m.UserId == userId);

            if (moduleToDelete == null)
            {
                _logger.LogError("There is no module to delete with {Id}", id);
                return new Result<bool>(new NullReferenceException());
            }

            _context.Modules.Remove(moduleToDelete);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfully deleted module with {Id} id", id);
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
            var moduleToUpdate = await _context.Modules.SingleOrDefaultAsync(m => m.Id == module.Id && m.UserId == module.UserId);

            if (moduleToUpdate == null)
            {
                _logger.LogError("There is no Module to update with {Id}", module.Id);
                return new Result<bool>(new ArgumentNullException());
            }
            
            await _context.SaveChangesAsync();

            _logger.LogInformation("Module with {Id} successfully updated", module.Id);
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }
}