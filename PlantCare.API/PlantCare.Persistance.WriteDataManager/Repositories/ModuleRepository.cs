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
    
    public async ValueTask<Result<int>> Add(int userId, string name, string address)
    {
        try
        {
            var module = new Module()
            {
                Name = name,
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
}