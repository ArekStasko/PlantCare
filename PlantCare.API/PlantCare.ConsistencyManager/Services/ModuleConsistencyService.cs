using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PlantCare.Domain.CommonContexts.ConsistencyManagerContexts;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class ModuleConsistencyService : IQueueConsumer<Module>
{
    private readonly IModuleConsistencyContext _context;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    private readonly ILogger<ModuleConsistencyService> _logger;

    public ModuleConsistencyService(IModuleConsistencyContext context, IMapper mapper, IDistributedCache cache, ILogger<ModuleConsistencyService> logger)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
        _logger = logger;
    }
    public async Task ConsumeAsync(Module message)
    {
        switch (message.Action)
                {
                    case ActionType.Add:
                    {
                        var module = _mapper.Map<PlantCare.Domain.Models.Module.Module>(message.ModuleData);
                        await _context.Modules.AddAsync(module);
                        await _context.SaveChangesAsync();
                        await ResetCacheModule(module.UserId);
                        return;
                    }
                    case ActionType.Delete:
                    {
                        var moduleId = message.ModuleData.Id;
                        var moduleToDelete = await _context.Modules.SingleOrDefaultAsync(m => m.Id == moduleId);

                        if (moduleToDelete == null)
                        {
                            _logger.LogError("There is no module with {id} id", moduleId);
                            return;
                        }
                        
                        _context.Modules.Remove(moduleToDelete);
                        await _context.SaveChangesAsync();
                        await ResetCacheModule(moduleToDelete.UserId);
                        return;
                    }
                    case ActionType.Update:
                    {
                        var module = _mapper.Map<PlantCare.Domain.Models.Module.Module>(message.ModuleData);
                        _context.Modules.Update(module);
                        await _context.SaveChangesAsync();
                        await ResetCacheModule(module.UserId);
                        return;
                    }
                    default:
                    {
                        _logger.LogError("Module Consistency service executes for not existing action: {action}", message.Action);
                        return;
                    }
                }
    }

    private async Task ResetCacheModule(int userId)
    {
        await _cache.RemoveAsync($"Modules-{userId}");
        _logger.LogInformation("Redis cache has been updated");
    }
}