using RabbitMQ.Client;

namespace PlantCare.MessageBroker.Providers.QueueChannelProvider;

public interface IQueueChannelProvider<TQueueMessage>
{
    public IModel GetChannel();
}