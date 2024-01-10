using PlantCare.Domain.Enums;
using PlantCare.Persistance.DAO.HumidityMeasurement;

namespace PlantCare.Persistance.DAO.Plant;

public class PlantDAO : IPlantDAO
{
    public int Id { get; set; }
    public int PlaceId { get; set; }
    public Guid ModuleId { get; set; }
    public string Name { get; set; } = "Name";
    public string Description { get; set; } = "Description";
    public PlantType Type { get; set; }
    public virtual ICollection<HumidityMeasurementDAO> HumidityMeasurements { get; set; }
}