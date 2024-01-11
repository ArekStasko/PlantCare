using LanguageExt.Common;
using PlantCare.Persistance.DAO.HumidityMeasurement;

namespace PlantCare.Persistance.WriteDataManager.Repositories.HumidityMeasurementRepository;

public interface IHumidityMeasurementRepository
{
    ValueTask<Result<bool>> Add(IHumidityMeasurementDAO humidityMeasurement);
}