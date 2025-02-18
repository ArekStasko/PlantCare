using PlantCare.Domain.Models.UserAssignable;

namespace PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Plant;

public interface IModule : IUserAssignable
{
    int Id { get; set; }
    bool IsMonitoring { get; set; }
    ICollection<HumidityMeasurement.HumidityMeasurement> HumidityMeasurements { get; set; }
    Plant? Plant { get; set; }
}