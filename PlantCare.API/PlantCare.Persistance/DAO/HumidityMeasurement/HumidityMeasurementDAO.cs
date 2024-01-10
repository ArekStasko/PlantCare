namespace PlantCare.Persistance.DAO.HumidityMeasurement;

public class HumidityMeasurementDAO : IHumidityMeasurementDAO
{
    public int Id { get; set; }
    public Guid ModuleId { get; set; }
    public int Humidity { get; set; }
    public DateTime MeasurementDate { get; set; }
}