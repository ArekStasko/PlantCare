using PlantCare.Persistance.DAO.HumidityMeasurement;
using PlantCare.Persistance.DAO.Plant;

namespace PlantCare.Persistance.DAO.Module;

public interface IModuleDAO
{
    Guid Id { get; set; }
    ICollection<HumidityMeasurementDAO> HumidityMeasurements { get; set; }
    PlantDAO? Plant { get; set; }
}