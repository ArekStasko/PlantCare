namespace PlantCare.Domain.Models.HumidityMeasurement;

public class HumidityMeasurement : IHumidityMeasurement
{
    public int Id { get; set; }
    public int ModuleId { get; set; }
    public int Humidity { get; set; }
    public DateTime MeasurementDate { get; set; }
}