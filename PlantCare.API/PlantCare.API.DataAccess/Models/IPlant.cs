namespace PlantCare.API.DataAccess.Models;

public interface IPlant
{
    int Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    string ImageUrl { get; set; }
    int RequiredMoistureLevel { get; set; }
    int MoistureLevel { get; set; }
    string ModuleId { get; set; }
}