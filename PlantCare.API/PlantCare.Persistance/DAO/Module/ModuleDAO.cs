using PlantCare.Persistance.DAO.HumidityMeasurement;
using PlantCare.Persistance.DAO.Plant;

namespace PlantCare.Persistance.DAO.Module;

public class ModuleDAO : IModuleDAO
{
    public Guid Id { get; set; }

    public virtual ICollection<HumidityMeasurementDAO> HumidityMeasurements { get; set; }

    public virtual PlantDAO? Plant { get; set; }
}