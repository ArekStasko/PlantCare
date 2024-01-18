using LanguageExt.Common;
using PlantCare.Domain.Models.HumidityMeasurement;

namespace PlantCare.Persistance.Interfaces.ReadRepositories;

public interface IReadHumidityMeasurementRepository
{
    ValueTask<Result<IReadOnlyCollection<IHumidityMeasurement>>> Get(Guid id);
}