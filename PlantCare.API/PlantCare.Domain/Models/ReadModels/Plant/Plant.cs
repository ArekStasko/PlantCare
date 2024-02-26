using PlantCare.Domain.Enums;

namespace PlantCare.Domain.Models.ReadModels.Plant;

public class Plant : IReadPlant
{
    public int Id { get; set; }
    public int ConsistencyId { get; set; }
    public int PlaceId { get; set; }
    public Guid ModuleId { get; set; }
    public string Name { get; set; } = "Name";
    public string Description { get; set; } = "Description";
    public PlantType Type { get; set; }
    public virtual ICollection<PlantCare.Domain.Models.HumidityMeasurement.HumidityMeasurement> HumidityMeasurements { get; set; }
}