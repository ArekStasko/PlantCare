using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.Services.CommandHandlers.ModuleCommandHandlers;
using PlantCare.API.Services.Queries.ModuleQueries;
using PlantCare.API.Services.QueryHandlers.ModuleQueryHandlers;
using PlantCare.API.Services.Requests.ModuleCommands;
using PlantCare.API.Services.Tests.Utils;

namespace PlantCare.API.Services.Tests;

public class ModulesHandlersTests
{
    [Test]
    public async Task AddModuleHandler_Should_AddModule()
    {
        AddModuleCommand moduleToAdd = new AddModuleCommand()
        {
        };

        var moduleRepoMock = Setups.GetSuccessfullModuleRepository();
        IMock<ILogger<AddModuleHandler>> loggerMock = new Mock<ILogger<AddModuleHandler>>();
        
        var handler = new AddModuleHandler(moduleRepoMock.Object, loggerMock.Object);
        var result = await handler.Handle(moduleToAdd, new CancellationToken());
        
        moduleRepoMock.Verify(repo => repo.Add(It.IsAny<Guid>()), Times.Once);
    }
    
    [Test]
    public async Task AddModuleHandler_ShouldNot_AddModule()
    {
        AddModuleCommand moduleToAdd = new AddModuleCommand()
        {
        };

        var moduleRepoMock = Setups.GetUnsuccessfullModuleRepository();
        IMock<ILogger<AddModuleHandler>> loggerMock = new Mock<ILogger<AddModuleHandler>>();
        
        var handler = new AddModuleHandler(moduleRepoMock.Object, loggerMock.Object);
        var result = await handler.Handle(moduleToAdd, new CancellationToken());
        
        moduleRepoMock.Verify(repo => repo.Add(It.IsAny<Guid>()), Times.Once);
        
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
    public async Task DeletePlantHandler_Should_DeletePlantById()
    {
        DeleteModuleCommand deletePlantCommand = new DeleteModuleCommand()
        {
            Id = Guid.NewGuid()
        };

        var moduleRepoMock = Setups.GetSuccessfullModuleRepository();
        IMock<ILogger<DeleteModuleHandler>> loggerMock = new Mock<ILogger<DeleteModuleHandler>>();

        var handler = new DeleteModuleHandler(moduleRepoMock.Object, loggerMock.Object);
        var result = await handler.Handle(deletePlantCommand, new CancellationToken());
        
        moduleRepoMock.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Once);
        result.Match<IActionResult>(succ =>
        {
            Assert.IsTrue(succ);
            return new EmptyResult();
        }, err =>
        {
            Assert.IsNull(err);
            return new EmptyResult();
        });
    }
    
    [Test]
    public async Task DeleteModuleHandler_ShouldNot_DeleteModuleById()
    {
        DeleteModuleCommand deletePlantCommand = new DeleteModuleCommand()
        {
            Id = Guid.NewGuid()
        };

        var moduleRepoMock = Setups.GetUnsuccessfullModuleRepository();
        IMock<ILogger<DeleteModuleHandler>> loggerMock = new Mock<ILogger<DeleteModuleHandler>>();

        var handler = new DeleteModuleHandler(moduleRepoMock.Object, loggerMock.Object);
        var result = await handler.Handle(deletePlantCommand, new CancellationToken());
        
        moduleRepoMock.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Once);
        result.Match<IActionResult>(succ =>
        {
            Assert.IsFalse(succ);
            return new EmptyResult();
        }, err =>
        {
            Assert.IsNotNull(err);
            return new EmptyResult();
        });
    }

    [Test]
    public async Task EditPlantHandler_Should_EditPlant()
    {
        UpdateModuleCommand moduleToUpdate = new UpdateModuleCommand()
        {
            Id = Guid.NewGuid(),
            CriticalMoistureLevel = 30,
            RequiredMoistureLevel = 130,
        };

        var moduleRepoMock = Setups.GetSuccessfullModuleRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<UpdateModuleHandler>> loggerMock = new Mock<ILogger<UpdateModuleHandler>>();

        var handler = new UpdateModuleHandler(moduleRepoMock.Object, loggerMock.Object, mapper);
        var result = await handler.Handle(moduleToUpdate, new CancellationToken());
        
        moduleRepoMock.Verify(repo => repo.Update(It.IsAny<IModule>()), Times.Once);
        result.Match<IActionResult>(succ =>
        {
            Assert.IsTrue(succ);
            return new EmptyResult();
        }, err =>
        {
            Assert.IsNull(err);
            return new EmptyResult();
        });
    }
    
    [Test]
    public async Task UpdateModuleHandler_ShouldNot_UpdateModule()
    {
        UpdateModuleCommand moduleToUpdate = new UpdateModuleCommand()
        {
            Id = Guid.NewGuid(),
            CriticalMoistureLevel = 30,
            RequiredMoistureLevel = 130,
        };

        var moduleRepoMock = Setups.GetUnsuccessfullModuleRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<UpdateModuleHandler>> loggerMock = new Mock<ILogger<UpdateModuleHandler>>();

        var handler = new UpdateModuleHandler(moduleRepoMock.Object, loggerMock.Object, mapper);
        var result = await handler.Handle(moduleToUpdate, new CancellationToken());
        
        moduleRepoMock.Verify(repo => repo.Update(It.IsAny<IModule>()), Times.Once);
        result.Match<IActionResult>(succ =>
        {
            Assert.IsFalse(succ);
            return new EmptyResult();
        }, err =>
        {
            Assert.IsNotNull(err);
            return new EmptyResult();
        });
    }
    
    [Test]
    public async Task GetModulesHandler_Should_ReturnModules()
    {
        var modulesRepoMock = Setups.GetSuccessfullModuleRepository();
        IMock<ILogger<GetModulesHandler>> loggerMock = new Mock<ILogger<GetModulesHandler>>();

        var handler = new GetModulesHandler(modulesRepoMock.Object, loggerMock.Object);
        var result = await handler.Handle(new GetModulesQuery(), new CancellationToken());
        
        modulesRepoMock.Verify(repo => repo.Get(), Times.Once);
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