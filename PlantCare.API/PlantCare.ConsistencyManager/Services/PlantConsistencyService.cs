using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class PlantConsistencyService : IQueueConsumer<Plant>
{
    public Task ConsumeAsync(Plant message)
    {
        throw new NotImplementedException();
    }
}