namespace PlantCare.Queries.Responses.HumidityMeasurements;

public record HumidityMeasurement
{
    public int Humidity { get; set; }
    public int BatteryLevel { get; set; }
    public DateTime Date { get; set; }
}