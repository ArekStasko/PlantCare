using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Persistance.ReadDataManager.Repositories;
using PlantCare.Tests.Mocks;

namespace PlantCare.Tests.ReadDataManager;

public class HumidityMeasurementRepositoryTests
{
    [Fact]
    public async void GetHumidityMeasurementsTest()
    {
        var moduleId = new Guid("1");
        var humidityMeasurementsDb = Services.humidityMeasurementsDb;
        var humidityMeasurementReadContext = Services.HumidityMeasurementReadContext();
        humidityMeasurementReadContext.Setup(x => x.HumidityMeasurements).Returns(humidityMeasurementsDb.Object);

        var repository = new HumidityMeasurementRepository(humidityMeasurementReadContext.Object,
            new Mock<ILogger<HumidityMeasurementRepository>>().Object);
        var result = await repository.Get(1);
        
        humidityMeasurementReadContext.Verify(x => x.HumidityMeasurements, Times.Once);
        result.Match(succ =>
        {
            Assert.Equal(1, succ.Count);
            return true;
        }, err =>
        {
            Assert.Fail();
            return false;
        });
    }
}