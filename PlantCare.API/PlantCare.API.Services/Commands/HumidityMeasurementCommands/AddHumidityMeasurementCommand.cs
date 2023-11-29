namespace PlantCare.API.Services.Requests.HumidityMeasurementCommands;

public class AddHumidityMeasurementCommand : IHttpPostCommand
{
    public Guid ModuleId { get; set; }

    public int Humidity { get; set; }

    public DateTime MeasurementDate { get; set; }
}