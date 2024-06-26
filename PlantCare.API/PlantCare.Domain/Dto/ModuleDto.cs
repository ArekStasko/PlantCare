using PlantCare.Domain.Models.HumidityMeasurement;

namespace PlantCare.Domain.Dto;

public class ModuleDto
{
    public Guid Id { get; set; }
    public int UserId { get; set; }

    public virtual ICollection<HumidityMeasurement> HumidityMeasurements { get; set; }
}