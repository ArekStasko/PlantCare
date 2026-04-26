using LanguageExt.Common;
using PlantCare.Domain.Models.HumidityMeasurement;

namespace PlantCare.Persistance.ReadDataManager.Repositories.Interfaces;

public interface IReadHumidityMeasurementRepository
{
    ValueTask<Result<IReadOnlyCollection<IHumidityMeasurement>>> Get(int id);
    Task<Result<(int, int)>> GetLatest(int id);
}