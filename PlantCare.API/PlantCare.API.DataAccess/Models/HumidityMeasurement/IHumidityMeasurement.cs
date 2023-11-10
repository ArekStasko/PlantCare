namespace PlantCare.API.DataAccess.Models.HumidityMeasurement;

public interface IHumidityMeasurement
{
    int Id { get; set; }
    int ModuleId { get; set; }
    int Humidity { get; set; }
    DateTime MeasurementDate { get; set; }
}