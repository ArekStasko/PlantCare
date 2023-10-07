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
        IMock<ILogger<EditPlantHandler>> loggerMock = new Mock<ILogger<EditPlantHandler>>();

        var handler = new EditPlantHandler(plantRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(plantToEdit, new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Edit(It.IsAny<IPlant>()), Times.Once);
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
        IMock<ILogger<EditPlantHandler>> loggerMock = new Mock<ILogger<EditPlantHandler>>();

        var handler = new EditPlantHandler(plantRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(plantToEdit, new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Edit(It.IsAny<IPlant>()), Times.Once);
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
    public async Task GetPlantsHandler_Should_ReturnPlants()
    {
        var plantRepoMock = Setups.GetSuccessfullPlantRepository();
        IMock<ILogger<GetPlantsHandler>> loggerMock = new Mock<ILogger<GetPlantsHandler>>();

        var handler = new GetPlantsHandler(plantRepoMock.Object, loggerMock.Object);
        var result = await handler.Handle(new GetPlantsQuery(), new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Get(), Times.Once);
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
    
    [Test]
    public async Task GetPlantHandler_Should_ReturnPlant()
    {
        var getPlantQuery = new GetPlantQuery()
        {
            Id = 1
        };
        
        var plantRepoMock = Setups.GetSuccessfullPlantRepository();
        IMock<ILogger<GetPlantHandler>> loggerMock = new Mock<ILogger<GetPlantHandler>>();

        var handler = new GetPlantHandler(plantRepoMock.Object, loggerMock.Object);
        var result = await handler.Handle(getPlantQuery, new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
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
    
    [Test]
    public async Task GetPlantHandler_ShouldNot_ReturnPlant()
    {
        var getPlantQuery = new GetPlantQuery()
        {
            Id = 1
        };
        
        var plantRepoMock = Setups.GetUnsuccessfullPlantRepository();
        IMock<ILogger<GetPlantHandler>> loggerMock = new Mock<ILogger<GetPlantHandler>>();

        var handler = new GetPlantHandler(plantRepoMock.Object, loggerMock.Object);
        var result = await handler.Handle(getPlantQuery, new CancellationToken());
        
        plantRepoMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
        result.Match<IActionResult>(succ =>
        {
            Assert.IsNull(succ);
            return new EmptyResult();
        }, err =>
        {
            Assert.IsNotNull(err);
            return new EmptyResult();
        });
    }
}