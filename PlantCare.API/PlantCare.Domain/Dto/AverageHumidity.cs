namespace PlantCare.Domain.Dto;

public record AverageHumidity()
{
    public DateTime Date { get; set; }
    public int Humidity { get; set; }
};