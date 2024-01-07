namespace PlantCare.Domain.Models.Place;
using PlantCare.Domain.Models.Plant;

public interface IPlace
{
    int Id { get; set; }
    string Name { get; set; }
    ICollection<Plant> Plants { get; }
}