using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Persistance.WriteDataManager.Interfaces;
using PlantCare.Persistance.WriteDataManager.Repositories;
using PlantCare.Tests.Mocks;

namespace PlantCare.Tests.WriteDataManager;

public class HumidityMeasurementRepositoryTests
{
    [Fact]
    public async void AddHumidityMeasurementTest()
    {
        HumidityMeasurement humidityMeasurement = new HumidityMeasurement()
        {
            Humidity = 31,
            MeasurementDate = DateTime.Now,
            Id = 1,
            ModuleId = Guid.NewGuid()
        };
        Mock<DbSet<HumidityMeasurement>> humidityMeasurementsDb = Services.humidityMeasurementsDb;
        Mock<IHumidityMeasurementWriteContext> humidityMeasurementWriteContext = Services.HumidityMeasurementWriteContext();
        humidityMeasurementsDb.Setup(x => x.AddAsync(humidityMeasurement, It.IsAny<CancellationToken>()));
        humidityMeasurementWriteContext.Setup(x => x.HumidityMeasurements).Returns(humidityMeasurementsDb.Object);
        humidityMeasurementWriteContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));

        var humidityMeasurementRepo = new HumidityMeasurementRepository(humidityMeasurementWriteContext.Object, new Mock<ILogger<HumidityMeasurementRepository>>().Object);

        await humidityMeasurementRepo.Add(humidityMeasurement);
        
        humidityMeasurementsDb.Verify(x => x.AddAsync(humidityMeasurement, It.IsAny<CancellationToken>()), Times.Once());
        humidityMeasurementWriteContext.Verify(x => x.HumidityMeasurements, Times.Once());
        humidityMeasurementWriteContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }
}