using PlantCare.Domain.Enums;
using PlantCare.Domain.Models.UserAssignable;

namespace PlantCare.Domain.Models.Plant;

public interface IPlant : IUserAssignable
{
    int Id { get; set; }
    int PlaceId { get; set; }
    int ModuleId { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    PlantType Type { get; set; }
}