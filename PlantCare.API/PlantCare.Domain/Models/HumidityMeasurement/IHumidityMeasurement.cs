namespace PlantCare.Domain.Models.HumidityMeasurement;

public interface IHumidityMeasurement
{
    int Id { get; set; }
    int ModuleId { get; set; }
    int Humidity { get; set; }
    public int BatteryLevel { get; set; }
    DateTime MeasurementDate { get; set; }
}