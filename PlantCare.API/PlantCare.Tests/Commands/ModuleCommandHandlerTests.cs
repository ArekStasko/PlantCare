using LanguageExt.ClassInstances.Pred;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Commands.CommandHandlers.ModuleCommandHandlers;
using PlantCare.Commands.Commands.Module;
using PlantCare.MessageBroker.Messages;
using PlantCare.Tests.Mocks;

namespace PlantCare.Tests.Commands;

public class ModuleCommandHandlerTests
{

    [Fact]
    public async void AddModuleHandler_Should_ReturnSuccess()
    {
        var command = new AddModuleCommand()
        {
            UserId = 1
        };

        var repository = Services.ModuleWriteRepository();
        var queueProducer = Services.ModuleQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<AddModuleHandler>>();

        repository.Setup(x => x.Add(It.IsAny<int>(), It.IsAny<Guid>()))
            .Returns(new ValueTask<Result<Guid>>(Guid.NewGuid()));
        queueProducer.Setup(x => x.PublishMessage(It.IsAny<Module>()));

        var handler = new AddModuleHandler(repository.Object, mapper, queueProducer.Object, logger.Object);
        var result = await handler.Handle(command, new CancellationToken());
        
        repository.Verify(x => x.Add(It.IsAny<int>(), It.IsAny<Guid>()), Times.Once);
        queueProducer.Verify(x => x.PublishMessage(It.IsAny<Module>()), Times.Once);

        result.Match(succ =>
        {
            Assert.NotNull(succ);
            return true;
        }, err =>
        {
            Assert.Fail();
            return false;
        });
    }
    
    [Fact]
    public async void AddModuleHandler_Should_ReturnError()
    {
        var command = new AddModuleCommand()
        {
            UserId = 1
        };

        var repository = Services.ModuleWriteRepository();
        var queueProducer = Services.ModuleQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<AddModuleHandler>>();

        repository.Setup(x => x.Add(It.IsAny<int>(), It.IsAny<Guid>()))
            .Throws(new Exception());
        queueProducer.Setup(x => x.PublishMessage(It.IsAny<Module>()));

        var handler = new AddModuleHandler(repository.Object, mapper, queueProducer.Object, logger.Object);
        var result = await handler.Handle(command, new CancellationToken());
        
        repository.Verify(x => x.Add(It.IsAny<int>(), It.IsAny<Guid>()), Times.Once);
        queueProducer.Verify(x => x.PublishMessage(It.IsAny<Module>()), Times.Never);

        result.Match(succ =>
        {
            Assert.Fail();
            return false;
        }, err => true);
    }
    
    [Fact]
    public async void DeleteModuleHandler_Should_ReturnSuccess()
    {
        var command = new DeleteModuleCommand()
        {
            Id = Guid.NewGuid(),
            UserId = 1
        };

        var repository = Services.ModuleWriteRepository();
        var queueProducer = Services.ModuleQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<DeleteModuleHandler>>();

        repository.Setup(x => x.Delete(It.IsAny<int>(), It.IsAny<Guid>()))
            .Returns(new ValueTask<Result<bool>>(true));
        queueProducer.Setup(x => x.PublishMessage(It.IsAny<Module>()));

        var handler = new DeleteModuleHandler(repository.Object, mapper, queueProducer.Object, logger.Object);
        var result = await handler.Handle(command, new CancellationToken());
        
        repository.Verify(x => x.Delete(It.IsAny<int>(), It.IsAny<Guid>()), Times.Once);
        queueProducer.Verify(x => x.PublishMessage(It.IsAny<Module>()), Times.Once);

        result.Match(succ =>
        {
            Assert.NotNull(succ);
            return true;
        }, err =>
        {
            Assert.Fail();
            return false;
        });
    }
    
    [Fact]
    public async void DeleteModuleHandler_Should_ReturnError()
    {
        var command = new DeleteModuleCommand()
        {
            Id = Guid.NewGuid(),
            UserId = 1
        };

        var repository = Services.ModuleWriteRepository();
        var queueProducer = Services.ModuleQueueProducer();
        var mapper = Services.CommandsMapper;
        var logger = new Mock<ILogger<DeleteModuleHandler>>();

        repository.Setup(x => x.Delete(It.IsAny<int>(), It.IsAny<Guid>()))
            .Throws(new Exception());
        queueProducer.Setup(x => x.PublishMessage(It.IsAny<Module>()));

        var handler = new DeleteModuleHandler(repository.Object, mapper, queueProducer.Object, logger.Object);
        var result = await handler.Handle(command, new CancellationToken());
        
        repository.Verify(x => x.Delete(It.IsAny<int>(), It.IsAny<Guid>()), Times.Once);
        queueProducer.Verify(x => x.PublishMessage(It.IsAny<Module>()), Times.Never);

        result.Match(succ =>
        {
            Assert.Fail();
            return false;
        }, err => true);
    }
}