using PlantCare.Persistance.DAO.Plant;

namespace PlantCare.Persistance.DAO.Place;

public interface IPlaceDAO
{
    int Id { get; set; }
    string Name { get; set; }
    ICollection<PlantDAO> Plants { get; }
}