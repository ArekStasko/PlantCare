namespace PlantCare.Domain.Dto;

public class HumidityMeasurementDto
{
    public int Id { get; set; }
    public Guid ModuleId { get; set; }
    public int Humidity { get; set; }
    public DateTime MeasurementDate { get; set; }
}