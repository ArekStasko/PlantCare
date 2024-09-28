using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Commands.CommandHandlers.PlaceCommandHandlers;
using PlantCare.Commands.Commands.Place;
using PlantCare.Tests.Mocks;
using Place = PlantCare.Domain.Models.Place.Place;

namespace PlantCare.Tests.Commands;

public class PlaceCommandHandlerTests
{
    [Fact]
    public async void CreatePlaceHandler_Should_ReturnSuccess()
    {
        var command = new CreatePlaceCommand()
        {
            Name = "",
            UserId = 1
        };

        var repository = Services.PlaceWriteRepository();
        var queueProducer = Services.PlaceQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<CreatePlaceHandler>>();

        repository.Setup(x => x.Create(It.IsAny<Place>())).Returns(new ValueTask<Result<int>>(1));
        queueProducer.Setup(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Place>()));

        var handler = new CreatePlaceHandler(repository.Object, mapper, queueProducer.Object, logger.Object);
        var result = await handler.Handle(command, new CancellationToken());
        
        repository.Verify(x => x.Create(It.IsAny<Place>()), Times.Once);
        queueProducer.Verify(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Place>()), Times.Once);

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
    public async void CreatePlaceHandler_Should_ReturnError()
    {
        var command = new CreatePlaceCommand()
        {
            Name = "",
            UserId = 1
        };

        var repository = Services.PlaceWriteRepository();
        var queueProducer = Services.PlaceQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<CreatePlaceHandler>>();

        repository.Setup(x => x.Create(It.IsAny<Place>())).Throws(new Exception());
        queueProducer.Setup(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Place>()));

        var handler = new CreatePlaceHandler(repository.Object, mapper, queueProducer.Object, logger.Object);
        var result = await handler.Handle(command, new CancellationToken());
        
        repository.Verify(x => x.Create(It.IsAny<Place>()), Times.Once);
        queueProducer.Verify(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Place>()), Times.Never);

        result.Match(succ =>
        {
            Assert.Fail();
            return false;
        }, err => true );
    }
    
    [Fact]
    public async void UpdatePlaceHandler_Should_ReturnSuccess()
    {
        var command = new UpdatePlaceCommand()
        {
            Id = 1,
            Name = "Test",
            UserId = 1
        };

        var repository = Services.PlaceWriteRepository();
        var queueProducer = Services.PlaceQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<UpdatePlaceHandler>>();

        repository.Setup(x => x.Update(It.IsAny<Place>())).Returns(new ValueTask<Result<bool>>(true));
        queueProducer.Setup(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Place>()));

        var handler = new UpdatePlaceHandler(repository.Object, mapper, queueProducer.Object, logger.Object);
        var result = await handler.Handle(command, new CancellationToken());
        
        repository.Verify(x => x.Update(It.IsAny<Place>()), Times.Once);
        queueProducer.Verify(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Place>()), Times.Once);

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
    public async void UpdatePlaceHandler_Should_ReturnError()
    {
        var command = new UpdatePlaceCommand()
        {
            Id = 1,
            Name = "Test",
            UserId = 1
        };

        var repository = Services.PlaceWriteRepository();
        var queueProducer = Services.PlaceQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<UpdatePlaceHandler>>();

        repository.Setup(x => x.Update(It.IsAny<Place>())).Throws(new Exception());
        queueProducer.Setup(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Place>()));

        var handler = new UpdatePlaceHandler(repository.Object, mapper, queueProducer.Object, logger.Object);
        var result = await handler.Handle(command, new CancellationToken());
        
        repository.Verify(x => x.Update(It.IsAny<Place>()), Times.Once);
        queueProducer.Verify(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Place>()), Times.Never);

        result.Match(succ =>
        {
            Assert.Fail();
            return false;
        }, err => true );
    }
    
    [Fact]
    public async void DeletePlaceHandler_Should_ReturnSuccess()
    {
        var command = new DeletePlaceCommand()
        {
            Id = 1,
            UserId = 1
        };

        var repository = Services.PlaceWriteRepository();
        var queueProducer = Services.PlaceQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<DeletePlaceHandler>>();

        repository.Setup(x => x.Delete(It.IsAny<int>(), It.IsAny<int>())).Returns(new ValueTask<Result<bool>>(true));
        queueProducer.Setup(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Place>()));

        var handler = new DeletePlaceHandler(repository.Object, mapper, queueProducer.Object, logger.Object);
        var result = await handler.Handle(command, new CancellationToken());
        
        repository.Verify(x => x.Delete(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        queueProducer.Verify(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Place>()), Times.Once);

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
    public async void DeletePlaceHandler_Should_ReturnError()
    {
        var command = new DeletePlaceCommand()
        {
            Id = 1,
            UserId = 1
        };

        var repository = Services.PlaceWriteRepository();
        var queueProducer = Services.PlaceQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<DeletePlaceHandler>>();

        repository.Setup(x => x.Delete(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());
        queueProducer.Setup(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Place>()));

        var handler = new DeletePlaceHandler(repository.Object, mapper, queueProducer.Object, logger.Object);
        var result = await handler.Handle(command, new CancellationToken());
        
        repository.Verify(x => x.Delete(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        queueProducer.Verify(x => x.PublishMessage(It.IsAny<MessageBroker.Messages.Place>()), Times.Never);

        result.Match(succ =>
        {
            Assert.Fail();
            return false;
        }, err => true );
    }
}