namespace PlantCare.API.DataAccess.Models.Module;

public interface IModule
{
    Guid Id { get; set; }
    int? RequiredMoistureLevel { get; set; }
    int? CriticalMoistureLevel { get; set; }
    ICollection<HumidityMeasurement.HumidityMeasurement> HumidityMeasurements { get; set; }
    Plant? Plant { get; set; }
}