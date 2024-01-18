using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.Services.Requests.PlaceCommands;

public class UpdatePlaceCommand : IHttpPostCommand
{
    public int Id { get; set; }
    public string Name { get; set; }
}