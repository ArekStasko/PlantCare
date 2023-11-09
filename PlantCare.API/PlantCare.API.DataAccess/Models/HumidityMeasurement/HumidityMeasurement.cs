using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.DataAccess.Models.HumidityMeasurement;

public class HumidityMeasurement : IHumidityMeasurement
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Humidity { get; set; }
    public DateTime MeasurementDate { get; set; }
}