using PlantCare.Domain.Enums;

namespace PlantCare.Commands.Commands.Plant;

public record CreatePlantCommand : IHttpPostCommand
{
    public string Name { get; set; } = "Name";
    public int UserId { get; set; }
    public string Description { get; set; } = "Description";
    public int PlaceId { get; set; }
    public PlantType Type { get; set; }
    public Guid? ModuleId { get; set; }
};