namespace PlantCare.Queries.Responses.HumidityMeasurements;

public record GetHumidityMeasurementsResponse
{
    public int Humidity { get; set; }

    public DateTime Date { get; set; }
}