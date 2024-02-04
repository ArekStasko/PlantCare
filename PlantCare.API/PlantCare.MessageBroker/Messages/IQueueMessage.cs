namespace PlantCare.MessageBroker.Messages;

public interface IQueueMessage
{
    Guid MessageId { get; set; }
    TimeSpan TimeToLive { get; set; }
}