namespace PlantCare.MessageBroker.Messages;

public class WaterSupply : IQueueMessage
{
    public Guid MessageId { get; set; } = Guid.NewGuid();
    public TimeSpan TimeToLive { get; set; } = TimeSpan.FromHours(24);
    public string QueueName { get; set; } = "WaterSupply";
    public ActionType Action { get; set; }
    public Domain.Models.Distributor.WaterSupply WaterSupplyDto { get; set; }
}