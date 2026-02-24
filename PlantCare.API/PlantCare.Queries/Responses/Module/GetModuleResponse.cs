using PlantCare.Domain.Models.Plant;

namespace PlantCare.Queries.Responses.Module;

public record GetModuleResponse
{
    public int Id { get; set; }
    public int? RequiredMoistureLevel { get; set; }
    public int? CriticalMoistureLevel { get; set; }
    public string? Name { get; set; }
}