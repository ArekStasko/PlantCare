namespace PlantCare.Queries.Abstraction.Responses.HumidityMeasurements;

public record GetHumidityMeasurementsResponse
{
    public int Humidity { get; set; }

    public DateTime Date { get; set; }
}