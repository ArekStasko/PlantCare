namespace PlantCare.Domain.Dto;

public class HumidityMeasurementDto
{
    public int Id { get; set; }
    public int ModuleId { get; set; }
    public int Humidity { get; set; }
    public DateTime MeasurementDate { get; set; }
}