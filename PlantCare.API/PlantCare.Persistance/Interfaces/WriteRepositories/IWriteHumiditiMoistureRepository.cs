using LanguageExt.Common;
using PlantCare.Domain.Models.HumidityMeasurement;

namespace PlantCare.Persistance.Interfaces.WriteRepositories;

public interface IWriteHumidityMeasurementRepository
{
    ValueTask<Result<bool>> Add(IHumidityMeasurement humidityMeasurement);
}