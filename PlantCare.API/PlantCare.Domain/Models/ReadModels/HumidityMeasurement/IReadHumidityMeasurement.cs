using PlantCare.Domain.Models.HumidityMeasurement;

namespace PlantCare.Domain.Models.ReadModels;

public interface IReadHumidityMeasurement : IHumidityMeasurement
{
    public int ConsistencyId { get; set; }
}