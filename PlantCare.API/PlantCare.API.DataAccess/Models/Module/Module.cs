using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.DataAccess.Models.Module;

[Table("Module")]
public class Module : IModule
{
    [Required]
    public int Id { get; set; }

    public int? CurrentMoistureLevel { get; set; }

    [Required]
    public int RequiredMoistureLevel { get; set; }

    [Required]
    public int CriticalMoistureLevel { get; set; }

    [Required]
    public virtual ICollection<HumidityMeasurement.HumidityMeasurement> HumidityMeasurements { get; set; }
    
    [Required]
    public virtual Plant Plant { get; set; }
}