using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlantCare.MessageBroker.Messages;
using PlantCare.MessageBroker.Providers.ChannelProvider;
using RabbitMQ.Client;

namespace PlantCare.MessageBroker.Producer;

public class QueueProducer<TQueueMessage> : IQueueProducer<TQueueMessage> where TQueueMessage : IQueueMessage
{
    private readonly ILogger<QueueProducer<TQueueMessage>> _logger;
    private readonly IModel _channel;

    public QueueProducer(ILogger<QueueProducer<TQueueMessage>> logger, IChannelProvider channelProvider)
    {
        _logger = logger;
        _channel = channelProvider.GetChannel();
    }
    
    public void PublishMessage(TQueueMessage message)
    {
        if (Equals(message, default(TQueueMessage))) throw new ArgumentException(nameof(message));
        if (message.TimeToLive.Ticks <= 0) throw new Exception("Time to live cannot be less or equal 0");

        message.MessageId = Guid.NewGuid();

        try
        {
            var serializedMessage = SerializeMessage(message);
            var queueName = message.QueueName;
            _logger.LogInformation("QueueProducer: publishing new message to {queueName} queue", queueName);
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.Type = queueName;
            properties.Expiration = message.TimeToLive.TotalMilliseconds.ToString();
            
            _channel.BasicPublish(queueName, queueName, properties, serializedMessage);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong while publishing queue message");
        }
    }
    
    private static byte[] SerializeMessage(TQueueMessage message)
    {
        var stringContent = JsonConvert.SerializeObject(message);
        return Encoding.UTF8.GetBytes(stringContent);
    }
}