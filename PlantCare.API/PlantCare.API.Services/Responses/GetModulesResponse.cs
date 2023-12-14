using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.Services.Responses;

public class GetModulesResponse
{
    public Guid Id { get; set; }
    public int? RequiredMoistureLevel { get; set; }
    public int? CriticalMoistureLevel { get; set; }
    public Plant? Plant { get; set; }
};