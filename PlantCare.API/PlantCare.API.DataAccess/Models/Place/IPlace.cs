namespace PlantCare.API.DataAccess.Models.Place;

public interface IPlace
{ 
    int Id { get; set; }
    string Name { get; set; }
    ICollection<Plant> Plants { get; set; }
}