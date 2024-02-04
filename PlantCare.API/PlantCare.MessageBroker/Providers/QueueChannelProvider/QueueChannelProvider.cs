using PlantCare.MessageBroker.Providers.ChannelProvider;
using RabbitMQ.Client;

namespace PlantCare.MessageBroker.Providers.QueueChannelProvider;

public class QueueChannelProvider<TQueueMessage> : IQueueChannelProvider<TQueueMessage>
{
    private readonly IChannelProvider _channelProvider;
    private IModel _channel;
    private readonly string _queueName;

    public QueueChannelProvider(IChannelProvider channelProvider, IModel channel)
    {
        _channelProvider = channelProvider;
        _channel = channel; 
    }

    public IModel GetChannel()
    {
        _channel = _channelProvider.GetChannel();
        DeclareQueue();
        return _channel;
    }

    private void DeclareQueue()
    {
        var queueArgs = new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", _queueName },
            { "x-dead-letter-routing-key", _queueName },
            { "x-queue-type", "quorum" },
            { "x-dead-letter-strategy", "at-least-once" },
            { "overflow", "reject-publish" }
        };

        _channel.ExchangeDeclare(_queueName, ExchangeType.Direct);
        _channel.QueueDeclare(_queueName, true, false, false, queueArgs);
        _channel.QueueBind(_queueName, _queueName, _queueName, null);
    }
}