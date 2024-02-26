namespace PlantCare.Domain.Models.ReadModels.HumidityMeasurement;

public class HumidityMeasurement : IReadHumidityMeasurement
{
    public int Id { get; set; }
    public int ConsistencyId { get; set; }
    public Guid ModuleId { get; set; }
    public int Humidity { get; set; }
    public DateTime MeasurementDate { get; set; }
}