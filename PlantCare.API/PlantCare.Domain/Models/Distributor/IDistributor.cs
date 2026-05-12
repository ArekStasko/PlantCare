using PlantCare.Domain.Models.Plant;

namespace PlantCare.Domain.Models.Distributor;

public interface IDistributor
{
    int Id { get; }
    int UserId { get; set; }
    List<IPlant> Plants { get; }
}