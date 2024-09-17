using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Commands.CommandHandlers.PlantCommandHandlers;
using PlantCare.Commands.Commands.Plant;
using PlantCare.Domain.Enums;
using PlantCare.Domain.Models.Plant;
using PlantCare.Tests.Mocks;

namespace PlantCare.Tests.Commands;

public class PlantCommandHandlerTests
{
    [Fact]
    public async void CreatePlantTest_ReturnsSuccess()
    {
        var command = new CreatePlantCommand()
        {
            Description = "",
            ModuleId = Guid.NewGuid(),
            Name = "",
            PlaceId = 1,
            Type = PlantType.Decorative,
            UserId = 1
        };
        
        var plantWriteRepository = Services.PlantWriteRepository();
        var plantQueueProducer = Services.PlantQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<CreatePlantHandler>>().Object;

        plantWriteRepository.Setup(x => x.Create(It.IsAny<Plant>())).Returns(new ValueTask<Result<int>>(1));
        plantQueueProducer.Setup(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Plant>()));
        
        var handler = new CreatePlantHandler(plantWriteRepository.Object, mapper, plantQueueProducer.Object, logger);
        var result = await handler.Handle(command, new CancellationToken());
        
        plantWriteRepository.Verify(x => x.Create(It.IsAny<Plant>()), Times.Once);
        plantQueueProducer.Verify(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Plant>()), Times.Once);
        
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
    
    [Fact]
    public async void CreatePlantTest_ReturnsError()
    {
        var command = new CreatePlantCommand()
        {
            Description = "",
            ModuleId = Guid.NewGuid(),
            Name = "",
            PlaceId = 1,
            Type = PlantType.Decorative,
            UserId = 1
        };
        
        var plantWriteRepository = Services.PlantWriteRepository();
        var plantQueueProducer = Services.PlantQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<CreatePlantHandler>>().Object;

        plantWriteRepository.Setup(x => x.Create(It.IsAny<Plant>())).Throws(new Exception());
        plantQueueProducer.Setup(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Plant>()));
        
        var handler = new CreatePlantHandler(plantWriteRepository.Object, mapper, plantQueueProducer.Object, logger);
        var result = await handler.Handle(command, new CancellationToken());
        
        plantWriteRepository.Verify(x => x.Create(It.IsAny<Plant>()), Times.Once);
        plantQueueProducer.Verify(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Plant>()), Times.Never);
        
        result.Match(succ =>
        {
            Assert.False(succ);
            return false;
        }, err => true
        );
    }
    
    [Fact]
    public async void DeletePlantTest_ReturnsSuccess()
    {
        var command = new DeletePlantCommand()
        {
            UserId = 1,
            Id = 1
        };
        
        var plantWriteRepository = Services.PlantWriteRepository();
        var plantQueueProducer = Services.PlantQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<DeletePlantHandler>>().Object;

        plantWriteRepository.Setup(x => x.Delete(It.IsAny<int>(), It.IsAny<int>())).Returns(new ValueTask<Result<bool>>(true));
        plantQueueProducer.Setup(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Plant>()));
        
        var handler = new DeletePlantHandler(plantWriteRepository.Object, mapper, plantQueueProducer.Object, logger);
        var result = await handler.Handle(command, new CancellationToken());
        
        plantWriteRepository.Verify(x => x.Delete(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        plantQueueProducer.Verify(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Plant>()), Times.Once);
        
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
    
    [Fact]
    public async void DeletePlantTest_ReturnsError()
    {
        var command = new DeletePlantCommand()
        {
            UserId = 1,
            Id = 1
        };
        
        var plantWriteRepository = Services.PlantWriteRepository();
        var plantQueueProducer = Services.PlantQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<DeletePlantHandler>>().Object;

        plantWriteRepository.Setup(x => x.Delete(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());
        plantQueueProducer.Setup(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Plant>()));
        
        var handler = new DeletePlantHandler(plantWriteRepository.Object, mapper, plantQueueProducer.Object, logger);
        var result = await handler.Handle(command, new CancellationToken());
        
        plantWriteRepository.Verify(x => x.Delete(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        plantQueueProducer.Verify(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Plant>()), Times.Never);
        
        result.Match(succ =>
            {
                Assert.False(succ);
                return false;
            }, err => true
        );
    }
}