using System.Collections.ObjectModel;
using LanguageExt.Common;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;

namespace PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;

public interface IHumidityMeasurementRepository
{
    ValueTask<Result<bool>> Add(IHumidityMeasurement humidityMeasurement);

    ValueTask<Result<IReadOnlyCollection<IHumidityMeasurement>>> Get(int id);
}