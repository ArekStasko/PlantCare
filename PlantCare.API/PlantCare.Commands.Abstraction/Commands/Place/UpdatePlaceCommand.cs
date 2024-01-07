namespace PlantCare.Commands.Abstraction.Commands.Place;

public record UpdatePlaceCommand : IHttpPostCommand
{
    public int Id { get; set; }
    public string Name { get; set; }
};