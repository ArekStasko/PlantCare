namespace PlantCare.API.Services.Requests;

public record DeletePlantCommand : IHttpDeleteCommand
{
    public int Id { get; set; }
}