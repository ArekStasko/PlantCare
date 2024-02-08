namespace PlantCare.MessageBroker.Messages;

public interface IQueueMessage
{
    Guid MessageId { get; set; }
    TimeSpan TimeToLive { get; set; }
    string QueueName { get; set; }
    ActionType Action { get; set; }
}