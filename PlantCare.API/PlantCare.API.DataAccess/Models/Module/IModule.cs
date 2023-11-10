namespace PlantCare.API.DataAccess.Models.Module;

public interface IModule
{
    int Id { get; set; }
    int PlantId { get; set; }
    int? CurrentMoistureLevel { get; set; }
    int RequiredMoistureLevel { get; set; }
    int CriticalMoistureLevel { get; set; }
    List<HumidityMeasurement.HumidityMeasurement> HumidityMeasurements { get; set; }
}