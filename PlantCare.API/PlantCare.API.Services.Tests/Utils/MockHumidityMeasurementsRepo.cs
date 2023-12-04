using LanguageExt.Common;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;

namespace PlantCare.API.Services.Tests.Utils;

public class MockHumidityMeasurementsRepo : IReadHumidityMeasurementRepository, IWriteHumidityMeasurementRepository
{
    public virtual ValueTask<Result<IReadOnlyCollection<IHumidityMeasurement>>> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual ValueTask<Result<bool>> Add(IHumidityMeasurement humidityMeasurement)
    {
        throw new NotImplementedException();
    }
}