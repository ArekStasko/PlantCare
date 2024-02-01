namespace PlantCare.Queries.Responses.Module;

public record GetCurrentMoistureResponse
{
    public int CurrentMoisture { get; set; }

    public DateTime Date { get; set; }
}