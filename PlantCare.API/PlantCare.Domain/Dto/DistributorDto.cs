using PlantCare.Domain.Models.Plant;

namespace PlantCare.Domain.Dto;

public class DistributorDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<IPlant> Plants { get; }
}