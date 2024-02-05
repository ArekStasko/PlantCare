using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Providers.QueueChannelProvider;
using RabbitMQ.Client;

namespace PlantCare.MessageBroker.Consumer;

public class QueueConsumerHandler<TMessageConsumer, TQueueMessage> : IQueueConsumerHandler<TMessageConsumer, TQueueMessage> where TMessageConsumer : IQueueConsumer<TQueueMessage> where TQueueMessage : class, IQueueMessage
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<QueueConsumerHandler<TMessageConsumer, TQueueMessage>> _logger;
    private readonly string _queueName;
    private IModel _consumerRegistrationChannel;
    private string _consumerTag;
    private readonly string _consumerName;

    public QueueConsumerHandler(IServiceProvider serviceProvider, ILogger<QueueConsumerHandler<TMessageConsumer, TQueueMessage>> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        _queueName = typeof(TQueueMessage).Name;
        _consumerName = typeof(TMessageConsumer).Name;
    }

    public void RegisterQueueConsumer()
    {
        var scope = _serviceProvider.CreateScope();
        
        // Can replace with new named services provided in .net8
        _consumerRegistrationChannel = scope.ServiceProvider.GetRequiredService<IQueueChannelProvider<TQueueMessage>>().GetChannel();
        
        
    }
    
    public Task ConsumeAsync(TQueueMessage message)
    {
        throw new NotImplementedException();
    }
}