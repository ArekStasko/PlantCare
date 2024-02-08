using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class ModuleConsistencyService : IQueueConsumer<ModuleMessage>
{
    public Task ConsumeAsync(ModuleMessage message)
    {
        throw new NotImplementedException();
    }
}