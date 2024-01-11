using LanguageExt.Common;
using PlantCare.Persistance.DAO.HumidityMeasurement;

namespace PlantCare.Persistance.Interfaces.ReadRepositories;

public interface IReadHumidityMeasurementRepository
{
    ValueTask<Result<IReadOnlyCollection<IHumidityMeasurementDAO>>> Get(Guid id);
}