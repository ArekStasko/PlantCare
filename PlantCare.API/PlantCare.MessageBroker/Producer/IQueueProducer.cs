using PlantCare.MessageBroker.Messages;

namespace PlantCare.MessageBroker.Producer;

public interface IQueueProducer<in TQueueMessage> where TQueueMessage : IQueueMessage
{
    void PublishMessage(TQueueMessage message);
}