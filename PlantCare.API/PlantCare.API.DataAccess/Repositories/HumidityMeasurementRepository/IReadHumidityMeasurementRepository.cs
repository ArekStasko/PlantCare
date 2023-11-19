using LanguageExt.Common;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;

namespace PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;

public interface IReadHumidityMeasurementRepository
{
    ValueTask<Result<IReadOnlyCollection<IHumidityMeasurement>>> Get(int id);
}