using PlantCare.Domain.Enums;

namespace PlantCare.Persistance.DAO.Plant;

public interface IPlantDAO
{
    int Id { get; set; }
    int PlaceId { get; set; }
    Guid ModuleId { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    PlantType Type { get; set; }
}