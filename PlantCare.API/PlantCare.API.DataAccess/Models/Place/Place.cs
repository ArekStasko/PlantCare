namespace PlantCare.API.DataAccess.Models.Place;

public class Place : IPlace
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Plant> Plants { get; set; }
}