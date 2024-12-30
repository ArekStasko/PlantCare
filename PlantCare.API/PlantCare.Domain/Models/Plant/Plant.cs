using PlantCare.Domain.Enums;

namespace PlantCare.Domain.Models.Plant;

public class Plant : IPlant
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PlaceId { get; set; }
    public int ModuleId { get; set; }
    public string Name { get; set; } = "Name";
    public string Description { get; set; } = "Description";
    public PlantType Type { get; set; }
    public virtual ICollection<HumidityMeasurement.HumidityMeasurement> HumidityMeasurements { get; set; }
}