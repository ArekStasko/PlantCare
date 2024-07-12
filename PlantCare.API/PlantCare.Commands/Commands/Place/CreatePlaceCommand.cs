namespace PlantCare.Commands.Commands.Place;

public record CreatePlaceCommand : IHttpPostCommand
{
    public string Name { get; set; }
    public int? UserId { get; set; }
};