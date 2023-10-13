namespace PlantCare.API.Services.Requests.PlaceCommands;

public class CreatePlaceCommand : IHttpPostCommand
{
    public string Name { get; set; }
}