namespace PlantCare.API.Services.Responses;

public class GetHumidityMeasurementsResponse
{
    public List<int> humidityMeasurementsValues { get; set; } = new();

    public List<DateTime> humidityMeasurementsDates { get; set; } = new();
}