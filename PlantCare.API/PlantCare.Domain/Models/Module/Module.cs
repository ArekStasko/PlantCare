namespace PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Plant;

public class Module : IModule
{
    public Guid Id { get; set; }

    public virtual ICollection<HumidityMeasurement.HumidityMeasurement> HumidityMeasurements { get; set; }

    public virtual Plant? Plant { get; set; }
}