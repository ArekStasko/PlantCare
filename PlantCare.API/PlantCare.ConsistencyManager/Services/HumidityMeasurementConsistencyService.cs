using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class HumidityMeasurementConsistencyService : IQueueConsumer<HumidityMeasurementMessage>
{
    public Task ConsumeAsync(HumidityMeasurementMessage message)
    {
        throw new NotImplementedException();
    }
}