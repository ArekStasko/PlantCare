namespace PlantCare.Commands.Commands.HumidityMeasurements;

public record AddHumidityMeasurementCommand : IHttpPostCommand
{
    public int ModuleId { get; set; }

    public int Humidity { get; set; }

    public DateTime MeasurementDate { get; set; }
};