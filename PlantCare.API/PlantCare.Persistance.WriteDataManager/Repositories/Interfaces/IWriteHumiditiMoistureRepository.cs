using LanguageExt.Common;
using PlantCare.Domain.Models.HumidityMeasurement;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWriteHumidityMeasurementRepository
{
    ValueTask<Result<int>> Add(IHumidityMeasurement humidityMeasurement);
}