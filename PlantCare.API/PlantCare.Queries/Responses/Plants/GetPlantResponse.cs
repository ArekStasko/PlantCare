using PlantCare.Domain.Enums;

namespace PlantCare.Queries.Responses.Plants;

public record GetPlantResponse
{
    public int Id { get; set; }
    public int PlaceId { get; set; }
    public int ModuleId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public PlantType Type { get; set; }
};