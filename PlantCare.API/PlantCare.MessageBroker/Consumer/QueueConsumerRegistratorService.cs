using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlantCare.MessageBroker.Messages;

namespace PlantCare.MessageBroker.Consumer;

public class QueueConsumerRegistratorService<TMessageConsumer, TQueueMessage> : IHostedService where TMessageConsumer : IQueueConsumer<TQueueMessage> where TQueueMessage : class, IQueueMessage
{
    private readonly ILogger<QueueConsumerRegistratorService<TMessageConsumer, TQueueMessage>> _logger;
    private IQueueConsumerHandler<TMessageConsumer, TQueueMessage> _consumerHandler;
    private readonly IServiceProvider _serviceProvider;
    private IServiceScope _scope;

    public QueueConsumerRegistratorService(ILogger<QueueConsumerRegistratorService<TMessageConsumer, TQueueMessage>> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _scope = _serviceProvider.CreateScope();

        _consumerHandler = _scope.ServiceProvider.GetRequiredService<IQueueConsumerHandler<TMessageConsumer, TQueueMessage>>();
        _consumerHandler.RegisterQueueConsumer();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Stop {nameof(QueueConsumerRegistratorService<TMessageConsumer, TQueueMessage>)}: Canceling {typeof(TMessageConsumer).Name} as Consumer for Queue {typeof(TQueueMessage).Name}");

        _consumerHandler.CancelQueueConsumer();

        _scope.Dispose();

        return Task.CompletedTask;
    }
}