namespace PlantCare.Commands.Commands.Distributor;

public class DeleteWaterSupplyCommand : IHttpPostCommand
{
    public int DistributorId { get; set; }
    public int PlantId { get; set; }
}