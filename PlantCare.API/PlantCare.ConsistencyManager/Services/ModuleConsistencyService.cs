using Microsoft.Extensions.Logging;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class ModuleConsistencyService : IQueueConsumer<Module>
{
    private readonly ILogger<ModuleConsistencyService> _logger;

    public ModuleConsistencyService(ILogger<ModuleConsistencyService> logger)
    {
        _logger = logger;
    }
    public Task ConsumeAsync(Module message)
    {
        return Task.CompletedTask;
    }
}