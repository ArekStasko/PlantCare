using PlantCare.Domain.Enums;

namespace PlantCare.Domain.Dto;

public class PlantDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PlaceId { get; set; }
    public Guid ModuleId { get; set; }
    public string Name { get; set; } = "Name";
    public string Description { get; set; } = "Description";
    public PlantType Type { get; set; }
}