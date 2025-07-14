using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Commands.CommandHandlers.HumidityMeasurementCommandHandlers;
using PlantCare.Commands.Commands.HumidityMeasurements;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Tests.Mocks;
using HumidityMeasurement = PlantCare.MessageBroker.Messages.HumidityMeasurement;

namespace PlantCare.Tests.Commands;
public class HumidityMeasurementCommandHandlerTests
{
    [Fact]
    public async void AddHumidityMeasurementHandler_Should_ReturnSuccess()
    {
        var command = new AddHumidityMeasurementCommand()
        {
            ModuleId = 1,
            Humidity = 91,
            MeasurementDate = DateTime.Now
        };

        var humidityMeasurementWriteRepository = Services.HumidityMeasurementRepository();
        var humidityMeasurementQueueProducer = Services.HumidityQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<AddHumidityMeasurementHandler>>().Object;

        humidityMeasurementWriteRepository.Setup(x => x.Add(It.IsAny<IHumidityMeasurement>()))
            .Returns(new Task<Result<int>>(() => new Result<int>(1)));
        humidityMeasurementQueueProducer.Setup(x => x.PublishMessage(It.IsAny<HumidityMeasurement>()));

        var handler = new AddHumidityMeasurementHandler(humidityMeasurementWriteRepository.Object, mapper, humidityMeasurementQueueProducer.Object, logger);
        var result = await handler.Handle(command, new CancellationToken());
        
        humidityMeasurementWriteRepository.Verify(x => x.Add(It.IsAny<IHumidityMeasurement>()), Times.Once);
        humidityMeasurementQueueProducer.Verify(x => x.PublishMessage(It.IsAny<HumidityMeasurement>()), Times.Once);

        result.Match(succ =>
        {
            Assert.True(succ);
            return true;
        }, err =>
        {
            Assert.Fail();
            return false;
        });
    }
}