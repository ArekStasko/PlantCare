using LanguageExt.Common;
using PlantCare.Domain.Models.HumidityMeasurement;

namespace PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

public interface IReadHumidityMeasurementRepository
{
    ValueTask<Result<IReadOnlyCollection<IHumidityMeasurement>>> Get(Guid id);
}