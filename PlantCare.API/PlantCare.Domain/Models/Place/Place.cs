namespace PlantCare.Domain.Models.Place;
using PlantCare.Domain.Models.Plant;

public class Place : IPlace
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Plant> Plants { get; } = new List<Plant>();
}