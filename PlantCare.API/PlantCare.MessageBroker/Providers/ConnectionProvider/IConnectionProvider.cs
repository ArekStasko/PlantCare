using RabbitMQ.Client;

namespace PlantCare.MessageBroker.Providers.ConnectionProvider;

public interface IConnectionProvider
{
    public IConnection GetConnection();
}