using PlantCare.Domain.Enums;

namespace PlantCare.Queries.Responses.HumidityMeasurements;

public class PlantHumidityStatus
{
    public int PlantId { get; set; }
    public HumidityStatus Status { get; set; }
};