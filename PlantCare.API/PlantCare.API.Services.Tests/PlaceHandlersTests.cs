using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess.Models.Place;
using PlantCare.API.Services.CommandHandlers.PlaceCommandHandlers;
using PlantCare.API.Services.Queries.PlaceQueries;
using PlantCare.API.Services.QueryHandlers.PlaceQueryHandlers;
using PlantCare.API.Services.Requests.PlaceCommands;
using PlantCare.API.Services.Tests.Utils;

namespace PlantCare.API.Services.Tests;

public class PlaceHandlersTests
{
    
    //TODO Add castle proxy configuration
    
    [Test]
    public async Task CreatePlaceHandler_Should_CreateOnePlace()
    {
        CreatePlaceCommand plantToCreate = new CreatePlaceCommand()
        {
            Name = "Test Name",
        };

        var placeRepoMock = Setups.GetSuccessfullPlaceRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<CreatePlaceHandler>> loggerMock = new Mock<ILogger<CreatePlaceHandler>>();
        
        var handler = new CreatePlaceHandler(placeRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(plantToCreate, new CancellationToken());
        
        placeRepoMock.Verify(repo => repo.Create(It.IsAny<IPlace>()), Times.Once);
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
    public async Task CreatePlaceHandler_ShouldNot_CreateOnePlace()
    {
        CreatePlaceCommand placeToCreate = new CreatePlaceCommand()
        {
            Name = "Test Name",
        };

        var placeRepoMock = Setups.GetUnsuccessfullPlaceRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<CreatePlaceHandler>> loggerMock = new Mock<ILogger<CreatePlaceHandler>>();
        
        var handler = new CreatePlaceHandler(placeRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(placeToCreate, new CancellationToken());
        
        placeRepoMock.Verify(repo => repo.Create(It.IsAny<IPlace>()), Times.Once);
        
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
    public async Task DeletePlaceHandler_Should_DeletePlaceById()
    {
        int IdOfPlaceToDelete = 1;
        DeletePlaceCommand deletePlaceCommand = new DeletePlaceCommand()
        {
            Id = IdOfPlaceToDelete
        };

        var placeRepoMock = Setups.GetSuccessfullPlaceRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<DeletePlaceHandler>> loggerMock = new Mock<ILogger<DeletePlaceHandler>>();

        var handler = new DeletePlaceHandler(placeRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(deletePlaceCommand, new CancellationToken());
        
        placeRepoMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
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
    public async Task DeletePlaceHandler_ShouldNot_DeletePlaceById()
    {
        int IdOfPlaceToDelete = 1;
        DeletePlaceCommand deletePlaceCommand = new DeletePlaceCommand()
        {
            Id = IdOfPlaceToDelete
        };

        var placeRepoMock = Setups.GetUnsuccessfullPlaceRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<DeletePlaceHandler>> loggerMock = new Mock<ILogger<DeletePlaceHandler>>();

        var handler = new DeletePlaceHandler(placeRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(deletePlaceCommand, new CancellationToken());
        
        placeRepoMock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
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
    public async Task UpdatePlaceHandler_Should_UpdatePlace()
    {
        UpdatePlaceCommand placeToUpdate = new UpdatePlaceCommand()
        {
            Id = 1,
            Name = "Test Name",
        };

        var placeRepoMock = Setups.GetSuccessfullPlaceRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<UpdatePlaceHandler>> loggerMock = new Mock<ILogger<UpdatePlaceHandler>>();

        var handler = new UpdatePlaceHandler(placeRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(placeToUpdate, new CancellationToken());
        
        placeRepoMock.Verify(repo => repo.Update(It.IsAny<IPlace>()), Times.Once);
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
    public async Task UpdatePlaceHandler_ShouldNot_UpdatePlace()
    {
        UpdatePlaceCommand placeToUpdate = new UpdatePlaceCommand()
        {
            Id = 1,
            Name = "Test Name",
        };

        var placeRepoMock = Setups.GetUnsuccessfullPlaceRepository();
        var mapper = Setups.GetMapper();
        IMock<ILogger<UpdatePlaceHandler>> loggerMock = new Mock<ILogger<UpdatePlaceHandler>>();

        var handler = new UpdatePlaceHandler(placeRepoMock.Object, mapper, loggerMock.Object);
        var result = await handler.Handle(placeToUpdate, new CancellationToken());
        
        placeRepoMock.Verify(repo => repo.Update(It.IsAny<IPlace>()), Times.Once);
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
    public async Task GetPlaceHandler_Should_ReturnPlace()
    {
        var placeRepoMock = Setups.GetSuccessfullPlaceRepository();
        IMock<ILogger<GetPlacesHandler>> loggerMock = new Mock<ILogger<GetPlacesHandler>>();

        var handler = new GetPlacesHandler(placeRepoMock.Object, loggerMock.Object);
        var result = await handler.Handle(new GetPlacesQuery(), new CancellationToken());
        
        placeRepoMock.Verify(repo => repo.Get(), Times.Once);
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