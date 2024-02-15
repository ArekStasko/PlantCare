using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class PlaceConsistencyService : IQueueConsumer<Place>
{
    public Task ConsumeAsync(Place message)
    {
        throw new NotImplementedException();
    }
}