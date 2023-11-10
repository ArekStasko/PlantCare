using System.ComponentModel.DataAnnotations;
using PlantCare.API.DataAccess.Enums;

namespace PlantCare.API.DataAccess.Models;

public interface IPlant
{
    int Id { get; set; }
    int PlaceId { get; set; }
    int ModuleId { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    PlantType Type { get; set; }
}