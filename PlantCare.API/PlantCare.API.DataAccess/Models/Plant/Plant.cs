using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlantCare.API.DataAccess.Enums;

namespace PlantCare.API.DataAccess.Models;

[Table("Plant")]
public class Plant : IPlant
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int PlaceId { get; set; }

    [Required]
    public Guid ModuleId { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = "Name";

    [Required]
    [MaxLength(550)]
    public string Description { get; set; } = "Description";

    [Required]
    public PlantType Type { get; set; }

    [Required]
    public virtual Module.Module Module { get; set; }
}