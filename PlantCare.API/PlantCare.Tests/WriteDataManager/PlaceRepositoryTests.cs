using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Domain.Models.Place;
using PlantCare.Domain.Models.Plant;
using PlantCare.Persistance.WriteDataManager.Interfaces;
using PlantCare.Persistance.WriteDataManager.Repositories;
using PlantCare.Tests.Mocks;

namespace PlantCare.Tests.WriteDataManager;

public class PlaceRepositoryTests
{
    [Fact]
    public async void CreatePlantTest()
    {
        Place place = new()
        {
            Id = 1,
            UserId = 1,
            Name = ""
        };
        Mock<DbSet<Place>> plantDb = Services.PlaceDb;
        Mock<IPlaceWriteContext> placeWriteContext = Services.PlaceWriteContext();
        
        placeWriteContext.Setup(x => x.Places).Returns(plantDb.Object);
        placeWriteContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));

        var plantRepo = new PlaceRepository(placeWriteContext.Object, new Mock<ILogger<PlaceRepository>>().Object);
        var result = await plantRepo.Create(place);
        
        placeWriteContext.Verify(x => x.Places.AddAsync(It.IsAny<Place>(), It.IsAny<CancellationToken>()), Times.Once());
        placeWriteContext.Verify(x => x.Places, Times.Once());
        placeWriteContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        result.Match(succ =>
        {
            Assert.Equal(place.Id, succ);
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
        int placeId = 1;
        int userId = 1;
        
        Mock<DbSet<Place>> plantDb = Services.PlaceDb;
        Mock<IPlaceWriteContext> placeWriteContext = Services.PlaceWriteContext();
        
        placeWriteContext.Setup(x => x.Places).Returns(plantDb.Object);
        placeWriteContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));

        var plantRepo = new PlaceRepository(placeWriteContext.Object, new Mock<ILogger<PlaceRepository>>().Object);
        var result = await plantRepo.Delete(userId, placeId);
        
        placeWriteContext.Verify(x => x.Places.Remove(It.IsAny<Place>()), Times.Once());
        placeWriteContext.Verify(x => x.Places, Times.AtLeast(2));
        placeWriteContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
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
        Place place = new()
        {
            Id = 1,
            UserId = 1,
            Name = "Test"
        };
        Mock<DbSet<Place>> plantDb = Services.PlaceDb;
        Mock<IPlaceWriteContext> placeWriteContext = Services.PlaceWriteContext();
        
        placeWriteContext.Setup(x => x.Places).Returns(plantDb.Object);
        placeWriteContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));

        var plantRepo = new PlaceRepository(placeWriteContext.Object, new Mock<ILogger<PlaceRepository>>().Object);
        var result = await plantRepo.Create(place);
        
        placeWriteContext.Verify(x => x.Places, Times.Once());
        placeWriteContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        result.Match(succ =>
        {
            Assert.Equal(place.Id, succ);
            return true;
        }, err =>
        {
            Assert.Fail();
            return false;
        });
    }
}