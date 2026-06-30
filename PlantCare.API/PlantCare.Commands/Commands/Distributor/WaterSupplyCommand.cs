namespace PlantCare.Commands.Commands.Distributor;

public class WaterSupplyCommand : IHttpPostCommand
{
    public int DistributorId { get; set; }
    public int UserId { get; set; }
}