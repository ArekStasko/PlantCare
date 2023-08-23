using System.ComponentModel.DataAnnotations;

namespace PlantCare.API.DataAccess.Models;

public class Plant : IPlant
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = "Name";
    
    [Required]
    [MaxLength(550)]
    public string Description { get; set; } = "Description";
    
    [Required] 
    public string ImageUrl { get; set; } = "Default URL";
    
    [Required]
    public int RequiredMoistureLevel { get; set; }
    
    public int MoistureLevel { get; set; } = 0;
    public string ModuleId { get; set; } = "";
}