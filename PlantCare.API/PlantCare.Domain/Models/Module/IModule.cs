namespace PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Plant;

public interface IModule
{
    Guid Id { get; set; }
    ICollection<HumidityMeasurement.HumidityMeasurement> HumidityMeasurements { get; set; }
    Plant? Plant { get; set; }
}