using PlantCare.Domain.Dto;

namespace PlantCare.MessageBroker.Messages;

public class HumidityMeasurement : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; } = TimeSpan.FromHours(24);
    public string QueueName { get; set; } = "HumidityMeasurement";
    public ActionType Action { get; set; }
    public HumidityMeasurementDto HumidityMeasurementData { get; set; }
}