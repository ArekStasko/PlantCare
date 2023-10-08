using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.Services.Requests.PlaceCommands;

public class EditPlaceCommand : IHttpPostCommand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Plant> Plants { get; set; }
}