namespace PlantCare.Domain.Models.Distributor;

public class WaterSupply
{
    public int Id { get; set; }
    public int DistributorId { get; set; }
    public int PlantId { get; set; }
    public int UserId { get; set; }
}