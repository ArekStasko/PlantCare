namespace PlantCare.Commands.Abstraction.Commands.Place;

public record CreatePlaceCommand : IHttpPostCommand
{
    public string Name { get; set; }
};