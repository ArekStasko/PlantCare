using System.ComponentModel.DataAnnotations;

namespace PlantCare.API.DataAccess.Models.Place;

public class Place : IPlace
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<RoomPlant> Plants { get; set; }
}

public class RoomPlant
{
    [Key]
    public int Id { get; set; }
    public Plant Plant { get; set; }
}