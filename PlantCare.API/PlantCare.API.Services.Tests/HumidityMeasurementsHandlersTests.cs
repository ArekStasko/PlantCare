using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.Services.CommandHandlers.HumidityMeasurementCommandHandlers;
using PlantCare.API.Services.Queries.HumidityMeasurementsQueries;
using PlantCare.API.Services.QueryHandlers.HumidityMeasurementsQueryHandlers;
using PlantCare.API.Services.Requests.HumidityMeasurementCommands;
using PlantCare.API.Services.Tests.Utils;

namespace PlantCare.API.Services.Tests;

public class HumidityMeasurementsHandlersTests
{
    [Test]
    public async Task AddHumidityMeasurementHandler_Should_AddHumidityMeasurement()
    {
        AddHumidityMeasurementCommand humidityMeasurementToAdd = new AddHumidityMeasurementCommand()
        {
        };

        var humidityMeasurementsRepoMock = Setups.GetSuccessfullHumidityMeasurementsRepository();
        IMock<ILogger<AddHumidityMeasurementCommandHandler>> loggerMock = new Mock<ILogger<AddHumidityMeasurementCommandHandler>>();
        var mapper = Setups.GetMapper();
        var handler = new AddHumidityMeasurementCommandHandler(humidityMeasurementsRepoMock.Object, loggerMock.Object, mapper);
        var result = await handler.Handle(humidityMeasurementToAdd, new CancellationToken());
        
        humidityMeasurementsRepoMock.Verify(repo => repo.Add(It.IsAny<IHumidityMeasurement>()), Times.Once);
    }
    
    [Test]
    public async Task AddHumidityMeasurementHandler_ShouldNot_AddHumidityMeasurement()
    {
        AddHumidityMeasurementCommand humidityMeasurementToAdd = new AddHumidityMeasurementCommand()
        {
        };

        var humidityMeasurementsRepoMock = Setups.GetUnsuccessfullHumidityMeasurementsRepository();
        IMock<ILogger<AddHumidityMeasurementCommandHandler>> loggerMock = new Mock<ILogger<AddHumidityMeasurementCommandHandler>>();
        var mapper = Setups.GetMapper();
        var handler = new AddHumidityMeasurementCommandHandler(humidityMeasurementsRepoMock.Object, loggerMock.Object, mapper);
        var result = await handler.Handle(humidityMeasurementToAdd, new CancellationToken());
        
        humidityMeasurementsRepoMock.Verify(repo => repo.Add(It.IsAny<IHumidityMeasurement>()), Times.Once);
        
        result.Match<IActionResult>(succ =>
        {
            Assert.IsNotNull(succ);
            return new EmptyResult();
        }, err =>
        {
            Assert.IsNotNull(err);
            return new EmptyResult();
        });
    }
    
    [Test]
    public async Task GetHumidityMeasurementsHandler_Should_ReturnHumidityMeasurements()
    {
        var humidityMeasurementsRepoMock = Setups.GetSuccessfullHumidityMeasurementsRepository();
        IMock<ILogger<GetHumidityMeasurementsHandler>> loggerMock = new Mock<ILogger<GetHumidityMeasurementsHandler>>();
        var handler = new GetHumidityMeasurementsHandler(humidityMeasurementsRepoMock.Object, loggerMock.Object);
        var result = await handler.Handle(new GetHumidityMeasurementQuery(), new CancellationToken());
        
        humidityMeasurementsRepoMock.Verify(repo => repo.Get(It.IsAny<Guid>()), Times.Once);
        result.Match<IActionResult>(succ =>
        {
            Assert.IsNotNull(succ);
            return new EmptyResult();
        }, err =>
        {
            Assert.IsNull(err);
            return new EmptyResult();
        });
    }
}