using PlantCare.MessageBroker.Messages;

namespace PlantCare.MessageBroker.Consumer;

public interface IQueueConsumer<in TQueueMessage> where TQueueMessage : class, IQueueMessage
{
    Task ConsumeAsync(TQueueMessage message);
}