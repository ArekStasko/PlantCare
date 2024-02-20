using LanguageExt.Common;
using PlantCare.Domain.Models.HumidityMeasurement;

namespace PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;

public interface IWriteHumidityMeasurementRepository
{
    ValueTask<Result<bool>> Add(IHumidityMeasurement humidityMeasurement);
}