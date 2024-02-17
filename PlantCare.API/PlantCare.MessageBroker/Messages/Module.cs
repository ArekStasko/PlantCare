using PlantCare.Domain.Dto;

namespace PlantCare.MessageBroker.Messages;

public class Module : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; } = TimeSpan.FromHours(24);
    public string QueueName { get; set; } = "Module";
    public ActionType Action { get; set; }
    public ModuleDto ModuleData { get; set; }
}