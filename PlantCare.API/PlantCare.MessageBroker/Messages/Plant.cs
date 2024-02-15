using PlantCare.Domain.Dto;

namespace PlantCare.MessageBroker.Messages;

public class Plant : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; }
    public string QueueName { get; set; }
    public ActionType Action { get; set; }
    public PlantDto PlantData { get; set; }
}