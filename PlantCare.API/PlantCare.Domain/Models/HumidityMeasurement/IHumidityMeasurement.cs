namespace PlantCare.Domain.Models.HumidityMeasurement;

public interface IHumidityMeasurement
{
    int Id { get; set; }
    Guid ModuleId { get; set; }
    int Humidity { get; set; }
    DateTime MeasurementDate { get; set; }
}