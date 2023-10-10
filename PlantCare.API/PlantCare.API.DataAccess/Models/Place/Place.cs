using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantCare.API.DataAccess.Models.Place;

public class Place : IPlace
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Plant> Plants { get; } = new List<Plant>();
}
