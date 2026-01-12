namespace PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Plant;

public class Module : IModule
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public bool IsMonitoring { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public virtual Plant? Plant { get; set; }
}