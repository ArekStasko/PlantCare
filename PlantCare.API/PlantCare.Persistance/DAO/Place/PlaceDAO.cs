using PlantCare.Persistance.DAO.Plant;

namespace PlantCare.Persistance.DAO.Place;

public class PlaceDAO : IPlaceDAO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<PlantDAO> Plants { get; } = new List<PlantDAO>();
}