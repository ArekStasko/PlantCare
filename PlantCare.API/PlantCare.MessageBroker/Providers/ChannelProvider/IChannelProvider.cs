using RabbitMQ.Client;

namespace PlantCare.MessageBroker.Providers.ChannelProvider;

public interface IChannelProvider
{
    public IModel GetChannel();
    public void Dispose();
}