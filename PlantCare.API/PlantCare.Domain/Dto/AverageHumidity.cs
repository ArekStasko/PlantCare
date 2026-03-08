namespace PlantCare.Domain.Dto;

public record AverageHumidity()
{
    public string Date { get; set; } = string.Empty;
    public int Humidity { get; set; }
};