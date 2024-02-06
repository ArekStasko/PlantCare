namespace PlantCare.MessageBroker.Consumer;

public interface IQueueConsumerHandler<TMessageConsumer, TQueueMessage>
{
    public void RegisterQueueConsumer();
    public void CancelQueueConsumer();
}