using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.Services.Handlers;
using PlantCare.API.Services.Requests;
using PlantCare.API.Services.Tests.Utils;

namespace PlantCare.API.Services.Tests;

public class HandlersTests
{
    // TODO: Make this tests work correctly, and test exact value that is returned from result
    
    [Test]
    public async Task CreatePlantHandler_Should_CreateOnePlant()
    {
        CreatePlantCommand plantToCreate = new CreatePlantCommand()
        {
            Name = "Test Name",
            Description = "Test Description",
            Type = 0,
            CriticalMoistureLevel = 30,
            RequiredMoistureLevel = 70,
            ModuleId = ""
        };

        var plantRepoMock = Setups.GetSuccessfullPlantRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<CreatePlantHandler>> loggerMock = new Mock<ILogger<CreatePlantHandler>>();
        
        var handler = new CreatePlantHandler(plantRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(plantToCreate, new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Create(It.IsAny<IPlant>()), Times.Once);
        Assert.IsTrue(result.IsSuccess);
    }
    
    [Test]
    public async Task CreatePlantHandler_ShouldNot_CreateOnePlant()
    {
        CreatePlantCommand plantToCreate = new CreatePlantCommand()
        {
            Name = "Test Name",
            Description = "Test Description",
            Type = 0,
            CriticalMoistureLevel = 30,
            RequiredMoistureLevel = 130,
            ModuleId = ""
        };

        var plantRepoMock = Setups.GetUnsuccessfullPlantRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<CreatePlantHandler>> loggerMock = new Mock<ILogger<CreatePlantHandler>>();
        
        var handler = new CreatePlantHandler(plantRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(plantToCreate, new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Create(It.IsAny<IPlant>()), Times.Once);
        Assert.IsFalse(result.IsSuccess);
    }

    [Test]
    public async Task DeletePlantHandler_Should_DeletePlantById()
    {
        int IdOfPlantToDelete = 1;
        DeletePlantCommand deletePlantCommand = new DeletePlantCommand()
        {
            Id = IdOfPlantToDelete
        };

        var plantRepoMock = Setups.GetSuccessfullPlantRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<DeletePlantHandler>> loggerMock = new Mock<ILogger<DeletePlantHandler>>();

        var handler = new DeletePlantHandler(plantRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(deletePlantCommand, new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        Assert.IsTrue(result.IsSuccess);
    }
    
    [Test]
    public async Task DeletePlantHandler_ShouldNot_DeletePlantById()
    {
        int IdOfPlantToDelete = 1;
        DeletePlantCommand deletePlantCommand = new DeletePlantCommand()
        {
            Id = IdOfPlantToDelete
        };

        var plantRepoMock = Setups.GetUnsuccessfullPlantRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<DeletePlantHandler>> loggerMock = new Mock<ILogger<DeletePlantHandler>>();

        var handler = new DeletePlantHandler(plantRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(deletePlantCommand, new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task EditPlantHandler_Should_EditPlant()
    {
        EditPlantCommand plantToEdit = new EditPlantCommand()
        {
            Id = 1,
            Name = "Test Name",
            Description = "Test Description",
            Type = 0,
            CriticalMoistureLevel = 30,
            RequiredMoistureLevel = 130,
            ModuleId = ""
        };

        var plantRepoMock = Setups.GetSuccessfullPlantRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<EditPlantCommand>> loggerMock = new Mock<ILogger<EditPlantCommand>>();

        var handler = new EditPlantHandler(plantRepoMock.Object, mapper, loggerMock.Object);
        var result = handler.Handle(plantToEdit, new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Edit(It.IsAny<IPlant>()), Times.Once);
    }
    
    [Test]
    public async Task EditPlantHandler_ShouldNot_EditPlant()
    {
        EditPlantCommand plantToEdit = new EditPlantCommand()
        {
            Id = 1,
            Name = "Test Name",
            Description = "Test Description",
            Type = 0,
            CriticalMoistureLevel = 30,
            RequiredMoistureLevel = 130,
            ModuleId = ""
        };

        var plantRepoMock = Setups.GetUnsuccessfullPlantRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<EditPlantCommand>> loggerMock = new Mock<ILogger<EditPlantCommand>>();

        var handler = new EditPlantHandler(plantRepoMock.Object, mapper, loggerMock.Object);
        var result = handler.Handle(plantToEdit, new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Edit(It.IsAny<IPlant>()), Times.Once);
    }
}