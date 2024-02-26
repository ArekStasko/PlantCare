using PlantCare.Domain.Models.Plant;

namespace PlantCare.Domain.Models.ReadModels;

public interface IReadPlant : IPlant
{
    public int ConsistencyId { get; set; }
}