using System.ComponentModel.DataAnnotations;
using PlantCare.API.DataAccess.Enums;

namespace PlantCare.API.DataAccess.Models;

public interface IPlant
{
    int Id { get; set; }
    int PlaceId { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    PlantType Type { get; set; }
    
    // Moisture level is stored as percentage value so it has 0-100 range, that is why i am using byte type
    byte CriticalMoistureLevel { get; set; }
    byte RequiredMoistureLevel { get; set; }
    byte MoistureLevel { get; set; }
    string? ModuleId { get; set; }
}