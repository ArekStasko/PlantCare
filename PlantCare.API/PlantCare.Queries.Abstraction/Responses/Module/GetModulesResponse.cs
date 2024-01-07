using PlantCare.Domain.Models.Plant;

namespace PlantCare.Queries.Abstraction.Responses.Module;

public record GetModulesResponse
{
    public Guid Id { get; set; }
    public int? RequiredMoistureLevel { get; set; }
    public int? CriticalMoistureLevel { get; set; }
    public Plant? Plant { get; set; }
}