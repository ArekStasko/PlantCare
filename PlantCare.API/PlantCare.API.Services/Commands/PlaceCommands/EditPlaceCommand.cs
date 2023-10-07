using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.Services.Requests.PlaceCommands;

public class EditPlaceCommand : IHttpPostCommand
{
    public string Name { get; set; }
    public List<IPlant> Plants { get; set; }
}