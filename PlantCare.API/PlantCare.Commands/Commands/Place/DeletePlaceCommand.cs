namespace PlantCare.Commands.Commands.Place;

public record DeletePlaceCommand : IHttpDeleteCommand
{
    public int Id { get; set; }
    public int UserId { get; set; }
}