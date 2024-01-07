namespace PlantCare.Commands.Abstraction.Commands.Place;

public record DeletePlaceCommand : IHttpDeleteCommand
{
    public int Id { get; set; }
}