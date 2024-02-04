using PlantCare.MessageBroker.Messages;

namespace PlantCare.MessageBroker.Consumer;

public class QueueConsumerHandler<TQueueMessage> : IQueueConsumer<TQueueMessage> where TQueueMessage : class, IQueueMessage
{
    public Task ConsumeAsync(TQueueMessage message)
    {
        throw new NotImplementedException();
    }
}