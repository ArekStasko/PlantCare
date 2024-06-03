namespace PlantCare.Commands.Commands.Place;

public record UpdatePlaceCommand : IHttpPostCommand
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
};