using LanguageExt.Common;
using PlantCare.Persistance.DAO.HumidityMeasurement;

namespace PlantCare.Persistance.ReadDataManager.Repositories.HumidityMeasurementRepository;

public interface IHumidityMeasurementRepository
{
    ValueTask<Result<IReadOnlyCollection<IHumidityMeasurementDAO>>> Get(Guid id);
}