namespace PlantCare.Commands.Abstraction.Commands.Plant;

public record DeletePlantCommand : IHttpDeleteCommand
{
    public int Id { get; set; }
};