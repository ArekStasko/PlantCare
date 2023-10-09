using System.ComponentModel.DataAnnotations;
using PlantCare.API.DataAccess.Enums;

namespace PlantCare.API.DataAccess.Models;

public class Plant : IPlant
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int PlaceId { get; set; }
    
    [Required]
    public virtual Place.Place Place { get; set; }
    
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = "Name";
    
    [Required]
    [MaxLength(550)]
    public string Description { get; set; } = "Description";

    [Required]
    public PlantType Type { get; set; }
    
    [Required]
    public byte CriticalMoistureLevel { get; set; }
    
    [Required]
    public byte RequiredMoistureLevel { get; set; }
    
    public byte MoistureLevel { get; set; } = 0;
    
    public string? ModuleId { get; set; }
}