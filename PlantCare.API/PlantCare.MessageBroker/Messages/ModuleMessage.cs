using PlantCare.Domain.Dto;

namespace PlantCare.MessageBroker.Messages;

public class ModuleMessage : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; }
    public string QueueName { get; set; }
    public ActionType Action { get; set; }
    public ModuleDto ModuleData { get; set; }
}