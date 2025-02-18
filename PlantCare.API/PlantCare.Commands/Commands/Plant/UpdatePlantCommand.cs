using PlantCare.Domain.Enums;

namespace PlantCare.Commands.Commands.Plant;

public record UpdatePlantCommand : IHttpPostCommand
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = "Name";
    public string Description { get; set; } = "Description";
    public int PlaceId { get; set; }
    public PlantType Type { get; set; }
    public int? ModuleId { get; set; }
}