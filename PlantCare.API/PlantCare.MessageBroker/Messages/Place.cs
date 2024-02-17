using PlantCare.Domain.Dto;

namespace PlantCare.MessageBroker.Messages;

public class Place : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; } = TimeSpan.FromHours(24);
    public string QueueName { get; set; } = "Place";
    public ActionType Action { get; set; }
    public PlaceDto PlaceData { get; set; }
}