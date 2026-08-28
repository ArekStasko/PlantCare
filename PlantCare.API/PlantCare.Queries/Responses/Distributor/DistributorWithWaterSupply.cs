namespace PlantCare.Queries.Responses.Distributor;

public class DistributorWithWaterSupply
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsWaterSupplyActive { get; set; }
}