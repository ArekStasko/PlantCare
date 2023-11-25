using PlantCare.API.DataAccess.Enums;
using PlantCare.API.DataAccess.Models.Place;

namespace PlantCare.API.Services.Requests;

public record CreatePlantCommand : IHttpPostCommand
{
    public string Name { get; set; } = "Name";
    public string Description { get; set; } = "Description";
    public int PlaceId { get; set; }
    public PlantType Type { get; set; }
    public byte CriticalMoistureLevel { get; set; }
    public byte RequiredMoistureLevel { get; set; }
    public Guid? ModuleId { get; set; }
}