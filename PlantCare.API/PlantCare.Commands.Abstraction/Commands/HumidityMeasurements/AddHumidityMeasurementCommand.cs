namespace PlantCare.Commands.Abstraction.Commands.HumidityMeasurements;

public record AddHumidityMeasurementCommand : IHttpPostCommand
{
    public Guid ModuleId { get; set; }

    public int Humidity { get; set; }

    public DateTime MeasurementDate { get; set; }
};