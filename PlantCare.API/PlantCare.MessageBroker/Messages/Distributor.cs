using PlantCare.Domain.Dto;

namespace PlantCare.MessageBroker.Messages;

public class Distributor : IQueueMessage
{
    public Guid MessageId { get; set; } = Guid.NewGuid();
    public TimeSpan TimeToLive { get; set; } = TimeSpan.FromHours(24);
    public string QueueName { get; set; } = "Distributor";
    public ActionType Action { get; set; }
    public DistributorDto DistributorData { get; set; }
}