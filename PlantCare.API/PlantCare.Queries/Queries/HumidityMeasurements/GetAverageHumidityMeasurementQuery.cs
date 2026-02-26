namespace PlantCare.Queries.Queries.HumidityMeasurements;

public class GetAverageHumidityMeasurementQuery
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int ModuleId { get; set; }
}