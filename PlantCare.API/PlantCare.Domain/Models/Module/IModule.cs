using PlantCare.Domain.Models.UserAssignable;

namespace PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Plant;

public interface IModule : IUserAssignable
{
    int Id { get; set; }
    bool IsMonitoring { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    Plant? Plant { get; set; }
}