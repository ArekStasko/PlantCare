using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class ModuleConsistencyService : IQueueConsumer<Module>
{
    public Task ConsumeAsync(Module message)
    {
        throw new NotImplementedException();
    }
}