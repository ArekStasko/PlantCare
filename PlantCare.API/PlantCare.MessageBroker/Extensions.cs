using Microsoft.Extensions.DependencyInjection;
using PlantCare.MessageBroker.Providers.ChannelProvider;
using PlantCare.MessageBroker.Providers.ConnectionProvider;
using PlantCare.MessageBroker.Providers.QueueChannelProvider;
using RabbitMQ.Client;

namespace PlantCare.MessageBroker;

public static class Extensions
{
    public static void AddMessageBroker(this IServiceCollection services)
    {
        services.AddSingleton<IAsyncConnectionFactory>(provider =>
        {
            var factory = new ConnectionFactory()
            {
                UserName = Environment.GetEnvironmentVariable("MessageBrokerUsername"),
                Password = Environment.GetEnvironmentVariable("MessageBrokerPassword"),
                HostName = Environment.GetEnvironmentVariable("MessageBrokerHostName"),
                Port = int.Parse(Environment.GetEnvironmentVariable("MessageBrokerPort")!),

                DispatchConsumersAsync = true,
                AutomaticRecoveryEnabled = true,
            };

            return factory;
        });

        services.AddSingleton<IConnectionProvider, ConnectionProvider>();
        services.AddScoped<IChannelProvider, ChannelProvider>();
        services.AddScoped(typeof(IQueueChannelProvider<>), typeof(QueueChannelProvider<>));
    }
}