using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Providers.ChannelProvider;
using PlantCare.MessageBroker.Providers.QueueChannelProvider;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

        _consumerRegistrationChannel = scope.ServiceProvider.GetRequiredService<IQueueChannelProvider<TQueueMessage>>().GetChannel();

        var consumer = new AsyncEventingBasicConsumer(_consumerRegistrationChannel);

        consumer.Received += HandleMessage;
        try
        {
            _consumerTag = _consumerRegistrationChannel.BasicConsume(_queueName, false, consumer);
        }
        catch (Exception ex)
        {
            var exMsg = $"BasicConsume failed for Queue '{_queueName}'";
            _logger.LogError(ex, exMsg);
        }
    }
    
    public void CancelQueueConsumer()
    {
        try
        {
            _consumerRegistrationChannel.BasicCancel(_consumerTag);
        }
        catch (Exception ex)
        {
            var message = $"Error canceling QueueConsumer registration for {_consumerName}";
            _logger.LogError(message, ex);
        }
    }
    
     private async Task HandleMessage(object ch, BasicDeliverEventArgs ea)
    {
        var consumerScope = _serviceProvider.CreateScope();

        var consumingChannel = ((AsyncEventingBasicConsumer)ch).Model;

        IModel producingChannel = null;
        try
        {
            producingChannel = consumerScope.ServiceProvider.GetRequiredService<IChannelProvider>()
                .GetChannel();

            var message = DeserializeMessage(ea.Body.ToArray());

            producingChannel.TxSelect();

            var consumerInstance = consumerScope.ServiceProvider.GetRequiredService<TMessageConsumer>();

            await consumerInstance.ConsumeAsync(message);

            if (producingChannel.IsClosed || consumingChannel.IsClosed)
            {
                throw new Exception("A channel is closed during processing");
            }

            producingChannel.TxCommit();

            consumingChannel.BasicAck(ea.DeliveryTag, false);
        }
        catch (Exception ex)
        {
            var msg = $"Cannot handle consumption of a {_queueName} by {_consumerName}'";
            _logger.LogError(ex, msg);
            RejectMessage(ea.DeliveryTag, consumingChannel, producingChannel);
        }
        finally
        {
            consumerScope.Dispose();
        }
    }
    
    private void RejectMessage(ulong deliveryTag, IModel consumeChannel, IModel scopeChannel)
    {
        try
        {
            if (scopeChannel != null)
            {
                scopeChannel.TxRollback();
            }

            consumeChannel.BasicReject(deliveryTag, false);
        }
        catch (Exception bex)
        {
            var bexMsg =
                $"BasicReject failed";
            _logger.LogCritical(bex, bexMsg);
        }
    }
    
    private static TQueueMessage DeserializeMessage(byte[] message)
    {
        var stringMessage = Encoding.UTF8.GetString(message);
        return JsonConvert.DeserializeObject<TQueueMessage>(stringMessage);
    }
}