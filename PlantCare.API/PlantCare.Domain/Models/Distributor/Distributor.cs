using PlantCare.Domain.Models.Plant;

namespace PlantCare.Domain.Models.Distributor;

public class Distributor : IDistributor
{
    public int Id { get; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public bool WaterSupply { get; set; }
}