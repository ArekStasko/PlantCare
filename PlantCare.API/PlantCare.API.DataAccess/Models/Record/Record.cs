using System.ComponentModel.DataAnnotations;

namespace PlantCare.API.DataAccess.Models.Record;

public class Record : IRecord
{
    [Key]
    public int PlantId { get; set; }
    
    [Required]
    public byte MoistureLevel { get; set; }
    
    [Required]
    public TimeSpan IrrigationDate { get; set; }
}