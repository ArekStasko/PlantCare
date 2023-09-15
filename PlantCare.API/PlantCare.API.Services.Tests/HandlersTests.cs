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
        CreatePlantRequest plantToCreate = new CreatePlantRequest()
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
        CreatePlantRequest plantToCreate = new CreatePlantRequest()
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
        DeletePlantRequest deletePlantRequest = new DeletePlantRequest()
        {
            Id = IdOfPlantToDelete
        };

        var plantRepoMock = Setups.GetSuccessfullPlantRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<DeletePlantHandler>> loggerMock = new Mock<ILogger<DeletePlantHandler>>();

        var handler = new DeletePlantHandler(plantRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(deletePlantRequest, new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        Assert.IsTrue(result.IsSuccess);
    }
    
    [Test]
    public async Task DeletePlantHandler_ShouldNot_DeletePlantById()
    {
        int IdOfPlantToDelete = 1;
        DeletePlantRequest deletePlantRequest = new DeletePlantRequest()
        {
            Id = IdOfPlantToDelete
        };

        var plantRepoMock = Setups.GetUnsuccessfullPlantRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<DeletePlantHandler>> loggerMock = new Mock<ILogger<DeletePlantHandler>>();

        var handler = new DeletePlantHandler(plantRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(deletePlantRequest, new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
    }
}