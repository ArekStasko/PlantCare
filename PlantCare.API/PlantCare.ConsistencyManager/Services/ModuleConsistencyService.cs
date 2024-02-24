using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;
using PlantCare.Persistance.ReadDataManager.Interfaces;

namespace PlantCare.ConsistencyManager.Services;

public class ModuleConsistencyService : IQueueConsumer<Module>
{
    private readonly IModuleReadContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ModuleConsistencyService> _logger;

    public ModuleConsistencyService(IModuleReadContext context, IMapper mapper, ILogger<ModuleConsistencyService> logger)
    {
        _context = context;
        _mapper = mapper;
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
                        return;
                    }
                    case ActionType.Update:
                    {
                        var module = _mapper.Map<PlantCare.Domain.Models.Module.Module>(message.ModuleData);
                        _context.Modules.Update(module);
                        await _context.SaveChangesAsync();
                        return;
                    }
                    default:
                    {
                        _logger.LogError("Module Consistency service executes for not existing action: {action}", message.Action);
                        return;
                    }
                }
    }
}