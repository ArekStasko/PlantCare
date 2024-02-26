using PlantCare.Domain.Models.Place;

namespace PlantCare.Domain.Models.ReadModels.Place;

public interface IReadPlace : IPlace
{
    public int ConsistencyId { get; set; }
}