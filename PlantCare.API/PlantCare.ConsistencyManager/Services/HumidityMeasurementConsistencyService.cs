using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.ConsistencyManager.Services;

public class HumidityMeasurementConsistencyService : IQueueConsumer<HumidityMeasurement>
{
    public Task ConsumeAsync(HumidityMeasurement message)
    {
        throw new NotImplementedException();
    }
}