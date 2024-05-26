using PlantCare.Domain.Models.UserAssignable;

namespace PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Plant;

public interface IModule : IUserAssignable
{
    Guid Id { get; set; }
    ICollection<HumidityMeasurement.HumidityMeasurement> HumidityMeasurements { get; set; }
    Plant? Plant { get; set; }
}