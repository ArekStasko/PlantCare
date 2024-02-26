
namespace PlantCare.Domain.Models.ReadModels.Place;

public class Place : IReadPlace
{
    public int Id { get; set; }
    public int ConsistencyId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Models.Plant.Plant> Plants { get; } = new List<Models.Plant.Plant>();
}