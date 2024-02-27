namespace PlantCare.Commands.Commands.Plant;

public record DeletePlantCommand : IHttpDeleteCommand
{
    public int Id { get; set; }
};