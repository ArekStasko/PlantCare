using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Domain.Enums;
using PlantCare.Domain.Models.Plant;
using PlantCare.Persistance.WriteDataManager.Interfaces;
using PlantCare.Persistance.WriteDataManager.Repositories;
using PlantCare.Tests.Mocks;

namespace PlantCare.Tests.WriteDataManager;

public class PlantRepositoryTests
{
    [Fact]
    public async void CreatePlantTest()
    {
        Plant plant = new()
        {
            Id = 2,
            UserId = 1,
            Description = "",
            ModuleId = Guid.NewGuid(),
            PlaceId = 1,
            Type = PlantType.Decorative
        };
        Mock<DbSet<Plant>> plantDb = Services.PlantDb;
        Mock<IPlantWriteContext> plantWriteContext = Services.PlantWriteContext();
        
        plantWriteContext.Setup(x => x.Plants).Returns(plantDb.Object);
        plantWriteContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));

        var plantRepo = new PlantRepository(plantWriteContext.Object, new Mock<ILogger<PlantRepository>>().Object);
        var result = await plantRepo.Create(plant);
        
        plantWriteContext.Verify(x => x.Plants.AddAsync(It.IsAny<Plant>(), It.IsAny<CancellationToken>()), Times.Once());
        plantWriteContext.Verify(x => x.Plants, Times.Once());
        plantWriteContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        result.Match(succ =>
        {
            Assert.Equal(plant.Id, succ);
            return true;
        }, err =>
        {
            Assert.Fail();
            return false;
        });
    }
    
    [Fact]
    public async void DeletePlantTest()
    {
        int plantId = 1;
        int userId = 1;
        Mock<DbSet<Plant>> plantDb = Services.PlantDb;
        Mock<IPlantWriteContext> plantWriteContext = Services.PlantWriteContext();
        
        plantWriteContext.Setup(x => x.Plants).Returns(plantDb.Object);
        plantWriteContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));

        var plantRepo = new PlantRepository(plantWriteContext.Object, new Mock<ILogger<PlantRepository>>().Object);
        var result = await plantRepo.Delete(plantId, userId);
        
        plantWriteContext.Verify(x => x.Plants.Remove(It.IsAny<Plant>()), Times.Once());
        plantWriteContext.Verify(x => x.Plants, Times.AtLeast(2));
        plantWriteContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
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
    public async void UpdatePlantTest()
    {
        Plant plant = new()
        {
            Id = 1,
            UserId = 1,
            Description = "",
            ModuleId = Guid.NewGuid(),
            PlaceId = 1,
            Type = PlantType.Decorative
        };
        Mock<DbSet<Plant>> plantDb = Services.PlantDb;
        Mock<IPlantWriteContext> plantWriteContext = Services.PlantWriteContext();
        
        plantWriteContext.Setup(x => x.Plants).Returns(plantDb.Object);
        plantWriteContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));

        var plantRepo = new PlantRepository(plantWriteContext.Object, new Mock<ILogger<PlantRepository>>().Object);
        var result = await plantRepo.Update(plant);
        
        plantWriteContext.Verify(x => x.Plants, Times.Once());
        plantWriteContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
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
}