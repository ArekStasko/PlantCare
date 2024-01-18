namespace PlantCare.API.Services.Requests.PlaceCommands;

public class DeletePlaceCommand : IHttpDeleteCommand
{
    public int Id { get; set; }
}