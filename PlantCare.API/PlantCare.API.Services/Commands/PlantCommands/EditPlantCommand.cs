using PlantCare.API.DataAccess.Enums;

namespace PlantCare.API.Services.Requests;

public record EditPlantCommand : IHttpPostCommand
{
    public int Id { get; set; }
    public string Name { get; set; } = "Name";
    public string Description { get; set; } = "Description";
    public int PlaceId { get; set; }
    public PlantType Type { get; set; }
    public byte CriticalMoistureLevel { get; set; }
    public byte RequiredMoistureLevel { get; set; }
    public string? ModuleId { get; set; }
}