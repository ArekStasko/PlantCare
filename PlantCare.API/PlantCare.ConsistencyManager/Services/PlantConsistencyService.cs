using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class PlantConsistencyService : IQueueConsumer<PlantMessage>
{
    public Task ConsumeAsync(PlantMessage message)
    {
        throw new NotImplementedException();
    }
}