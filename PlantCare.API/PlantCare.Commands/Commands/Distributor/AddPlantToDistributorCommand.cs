namespace PlantCare.Commands.Commands.Distributor;

public class AddPlantToDistributorCommand : IHttpPostCommand
{
    public int UserId { get; set; }
    public int DistributorId { get; set; }
    public int PlantId { get; set; }
}