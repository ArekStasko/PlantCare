using LanguageExt.Common;
using PlantCare.Domain.Models.HumidityMeasurement;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWriteHumidityMeasurementRepository
{
    Task<Result<int>> Add(IHumidityMeasurement humidityMeasurement);
}