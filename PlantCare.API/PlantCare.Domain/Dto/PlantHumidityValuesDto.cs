namespace PlantCare.Domain.Dto;

public class PlantHumidityValuesDto
{
    public int PlantId { get; set; }
    public int minHumidity { get; set; }
    public int maxHumidity { get; set; }
}