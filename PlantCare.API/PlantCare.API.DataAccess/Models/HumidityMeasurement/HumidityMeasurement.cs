using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.DataAccess.Models.HumidityMeasurement;

[Table("HumidityMeasurement")]
public class HumidityMeasurement : IHumidityMeasurement
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }

    [Required]
    public Guid ModuleId { get; set; }

    [Required]
    public int Humidity { get; set; }

    [Required]
    public DateTime MeasurementDate { get; set; }
}