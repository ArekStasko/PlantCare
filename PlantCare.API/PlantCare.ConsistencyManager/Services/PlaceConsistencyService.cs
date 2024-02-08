using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class PlaceConsistencyService : IQueueConsumer<PlaceMessage>
{
    public Task ConsumeAsync(PlaceMessage message)
    {
        throw new NotImplementedException();
    }
}