namespace PlantCare.API.Services.Requests.HumidityMeasurementCommands;

public class AddHumidityMeasurementCommand : IHttpPostCommand
{
    public int ModuleId { get; set; }

    public int Humidity { get; set; }

    public DateTime MeasurementDate { get; set; }
}