using Microsoft.Extensions.DependencyInjection;
using PlantCare.MessageBroker.Consumer;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Providers.ChannelProvider;
using PlantCare.MessageBroker.Providers.ConnectionProvider;
using PlantCare.MessageBroker.Providers.QueueChannelProvider;
using PlantCare.MessageBroker.Producer;
using RabbitMQ.Client;

namespace PlantCare.MessageBroker;

public static class Extensions
{
    public static void AddQueueMessageConsumer<TMessageConsumer, TQueueMessage>(this IServiceCollection services) where TMessageConsumer : IQueueConsumer<TQueueMessage> where TQueueMessage : class, IQueueMessage
    {
        services.AddScoped(typeof(TMessageConsumer));
        services.AddScoped<IQueueConsumerHandler<TMessageConsumer, TQueueMessage>, QueueConsumerHandler<TMessageConsumer, TQueueMessage>>();
        services.AddHostedService<QueueConsumerRegistratorService<TMessageConsumer, TQueueMessage>>();
    }
    public static void AddMessageBroker(this IServiceCollection services)
    {
        var userName = Environment.GetEnvironmentVariable("MessageBrokerUsername");
        var password = Environment.GetEnvironmentVariable("MessageBrokerPassword");
        var hostName = Environment.GetEnvironmentVariable("MessageBrokerHostName");
        var port = int.Parse(Environment.GetEnvironmentVariable("MessageBrokerPort")!);
        
        services.AddSingleton<IAsyncConnectionFactory>(provider =>
        {
            ConnectionFactory factory = new();
            factory.Uri = new Uri($"amqp://{userName}:{password}@{hostName}:{port}");
            factory.ClientProvidedName = "PlantCare API";
            return factory;
        });

        services.AddSingleton<IConnectionProvider, ConnectionProvider>();
        services.AddScoped<IChannelProvider, ChannelProvider>();
        services.AddScoped(typeof(IQueueChannelProvider<>), typeof(QueueChannelProvider<>));
        services.AddScoped(typeof(IQueueProducer<>), typeof(QueueProducer<>));
    }
}