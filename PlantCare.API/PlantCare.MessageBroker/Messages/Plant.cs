using PlantCare.Domain.Dto;

namespace PlantCare.MessageBroker.Messages;

public class Plant : IQueueMessage
{
    public Guid MessageId { get; set; } = Guid.NewGuid();
    public TimeSpan TimeToLive { get; set; } = TimeSpan.FromHours(24);
    public string QueueName { get; set; } = "Plant";
    public ActionType Action { get; set; }
    public PlantDto PlantData { get; set; }
}