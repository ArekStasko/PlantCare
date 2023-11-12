using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess.Enums;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.DataAccess.Tests.Utils;

namespace PlantCare.API.DataAccess.Tests;

public class Tests
{
    private Mock<IPlantContext> _plantContextMock = Setups.SetupPlantContext();
    private IMock<ILogger<IPlantRepository>> _loggerMock = new Mock<ILogger<IPlantRepository>>();
    private IMapper _mapper = Setups.SetupMapper();

    [Test]
    public async Task Create_Should_CreateOnePlant()
    {
        IPlant plantToCreate = new Plant()
        {
            Name = "Test Name",
            Description = "Test Description",
            PlaceId = 1,
            Type = PlantType.Fruit,
        };
        
        

        var plantRepository = new PlantRepository(_plantContextMock.Object, _loggerMock.Object);
        var result = await plantRepository.Create(plantToCreate);
        
        _plantContextMock.Verify(dbSet => dbSet.Plants.AddAsync(It.IsAny<Plant>(), It.IsAny<CancellationToken>()), Times.Once);
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
    public async Task Delete_Should_DeletePlant()
    {
        IPlant plantToDelete = new Plant()
        {
            Id = 1,
            Name = "Test Name",
            Description = "Test Description",
            PlaceId = 1,
            Type = PlantType.Fruit,
        };

        var mockSet = Setups.GetPlantMockData();
        _plantContextMock.Setup(_ => _.Plants).Returns(mockSet.Object);
        
        var plantRepository = new PlantRepository(_plantContextMock.Object, _loggerMock.Object);
        var result = await plantRepository.Delete(plantToDelete.Id);
        
        _plantContextMock.Verify(dbSet => dbSet.Plants.Remove(It.IsAny<Plant>()), Times.Once);
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
    public async Task Update_Should_UpdatePlant()
    {
        IPlant EditedPlant = new Plant()
        {
            Id = 1,
            Name = "Test Name UPDATED",
            Description = "Test Description UPDATED",
            PlaceId = 1,
            Type = PlantType.Fruit,
        };

        var mockSet = Setups.GetPlantMockData();
        _plantContextMock.Setup(_ => _.Plants).Returns(mockSet.Object);

        var plantRepository = new PlantRepository(_plantContextMock.Object, _loggerMock.Object);
        var result = await plantRepository.Update(EditedPlant);
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
    public async Task Get_Should_ReturnPlants()
    {
        var mockSet = Setups.GetPlantMockData();
        _plantContextMock.Setup(_ => _.Plants).Returns(mockSet.Object);

        var plantRepository = new PlantRepository(_plantContextMock.Object, _loggerMock.Object);
        var result = await plantRepository.Get();
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
    public async Task GetById_Should_ReturnPlant()
    {
        var plantIdToGet = 1;
        
        var mockSet = Setups.GetPlantMockData();
        _plantContextMock.Setup(_ => _.Plants).Returns(mockSet.Object);

        var plantRepository = new PlantRepository(_plantContextMock.Object, _loggerMock.Object);
        var result = await plantRepository.Get(plantIdToGet);
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