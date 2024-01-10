namespace PlantCare.Persistance.DAO.HumidityMeasurement;

public interface IHumidityMeasurementDAO
{
    int Id { get; set; }
    Guid ModuleId { get; set; }
    int Humidity { get; set; }
    DateTime MeasurementDate { get; set; }
}