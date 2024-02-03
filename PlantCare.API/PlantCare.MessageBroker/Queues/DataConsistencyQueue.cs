using RabbitMQ.Client;

namespace PlantCare.MessageBroker.Queues;

public class DataConsistencyQueue
{
    public IModel SetupQueue()
    {
        var factory = new ConnectionFactory { HostName = Environment.GetEnvironmentVariable("MessageBrokerHostName") };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "ConsistencyQueue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        return channel;
    }
}