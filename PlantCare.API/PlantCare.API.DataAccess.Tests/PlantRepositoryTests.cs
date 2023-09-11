using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess.Enums;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.DataAccess.Tests.Utils;

namespace PlantCare.API.DataAccess.Tests;

public class Tests
{
    private Mock<PlantContext> _dataContextMock = Setups.SetupDataContext();
    private IMock<ILogger<IPlantRepository>> _loggerMock = new Mock<ILogger<IPlantRepository>>();
    private IMapper _mapper = Setups.SetupMapper();

    [Test]
    public async Task Create_Should_CreateOnePlant()
    {
        IPlant plantToCreate = new Plant()
        {
            Name = "Test Name",
            Description = "Test Description",
            Type = PlantType.Fruit,
            CriticalMoistureLevel = 30,
            RequiredMoistureLevel = 70,
            ModuleId = "da2r094hafhn"
        };
        
        

        var plantRepository = new PlantRepository(_dataContextMock.Object, _loggerMock.Object, _mapper);
        var result = await plantRepository.Create(plantToCreate);
        
        _dataContextMock.Verify(dbSet => dbSet.Plants.AddAsync(It.IsAny<Plant>(), It.IsAny<CancellationToken>()), Times.Once);
        
        Assert.IsTrue(result.IsSuccess);
    }

    [Test]
    public async Task Delete_Should_DeletePlant()
    {
        IPlant plantToDelete = new Plant()
        {
            Id = 1,
            Name = "Test Name",
            Description = "Test Description",
            Type = PlantType.Fruit,
            CriticalMoistureLevel = 30,
            RequiredMoistureLevel = 70,
            ModuleId = "da2r094hafhn"
        };

        var mockSet = Setups.GetMockData();
        _dataContextMock.Setup(_ => _.Plants).Returns(mockSet.Object);
        
        var plantRepository = new PlantRepository(_dataContextMock.Object, _loggerMock.Object, _mapper);
        var result = await plantRepository.Delete(plantToDelete.Id);
        
        _dataContextMock.Verify(dbSet => dbSet.Plants.Remove(It.IsAny<Plant>()), Times.Once);
        Assert.IsTrue(result.IsSuccess);
    }

    [Test]
    public async Task Edit_Should_EditPlant()
    {
        IPlant EditedPlant = new Plant()
        {
            Id = 1,
            Name = "Test Name UPDATED",
            Description = "Test Description UPDATED",
            Type = PlantType.Fruit,
            CriticalMoistureLevel = 40,
            RequiredMoistureLevel = 80,
            ModuleId = "da2r094hafhn"
        };

        var mockSet = Setups.GetMockData();
        _dataContextMock.Setup(_ => _.Plants).Returns(mockSet.Object);

        var plantRepository = new PlantRepository(_dataContextMock.Object, _loggerMock.Object, _mapper);
        var result = await plantRepository.Edit(EditedPlant);
        
        Assert.IsTrue(result.IsSuccess);
    }

    [Test]
    public async Task Get_Should_ReturnPlants()
    {
        var mockSet = Setups.GetMockData();
        _dataContextMock.Setup(_ => _.Plants).Returns(mockSet.Object);

        var plantRepository = new PlantRepository(_dataContextMock.Object, _loggerMock.Object, _mapper);
        var result = await plantRepository.Get();
        
        Assert.IsTrue(result.IsSuccess);
    }
    
    [Test]
    public async Task GetById_Should_ReturnPlants()
    {
        var plantIdToGet = 1;
        
        var mockSet = Setups.GetMockData();
        _dataContextMock.Setup(_ => _.Plants).Returns(mockSet.Object);

        var plantRepository = new PlantRepository(_dataContextMock.Object, _loggerMock.Object, _mapper);
        var result = await plantRepository.Get(plantIdToGet);
        
        Assert.IsTrue(result.IsSuccess);
    }
}