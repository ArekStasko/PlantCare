using LanguageExt.Common;
using PlantCare.Persistance.DAO.HumidityMeasurement;

namespace PlantCare.Persistance.Interfaces.WriteRepositories;

public interface IWriteHumidityMeasurementRepository
{
    ValueTask<Result<bool>> Add(IHumidityMeasurementDAO humidityMeasurement);
}