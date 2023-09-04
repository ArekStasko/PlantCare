using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess.Enums;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.DataAccess.Tests.Utils;

namespace PlantCare.API.DataAccess.Tests;

public class Tests
{
    private Mock<DataContext> _dataContextMock = Setups.SetupDataContext();
    private IMock<ILogger<IPlantRepository>> _loggerMock = new Mock<ILogger<IPlantRepository>>();
    private IMapper _mapper = Setups.SetupMapper();

    [Test]
    public void Create_Should_CreateOnePlant()
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
        var result = plantRepository.Create(plantToCreate);
        
        _dataContextMock.Verify(dbSet => dbSet.Plants.AddAsync(It.IsAny<Plant>(), It.IsAny<CancellationToken>()), Times.Once);
        
        Assert.IsTrue(result.IsCompletedSuccessfully);
    }

    [Test]
    public void Delete_Should_DeletePlant()
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
        var result = plantRepository.Delete(plantToDelete.Id);
        
        _dataContextMock.Verify(dbSet => dbSet.Plants.Remove(It.IsAny<Plant>()), Times.Once);
        
        Assert.IsTrue(result.IsCompletedSuccessfully);
    }
}