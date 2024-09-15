using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Persistance.ReadDataManager.Repositories;
using PlantCare.Tests.Mocks;

namespace PlantCare.Tests.ReadDataManager;

public class PlaceRepositoryTests
{
    [Fact]
    public async void GetPlacesTest()
    {
        var userId = 1;
        var placeDb = Services.PlaceDb;
        var placeReadContext = Services.PlaceReadContext();
        placeReadContext.Setup(x => x.Places).Returns(placeDb.Object);

        var repository = new PlaceRepository(placeReadContext.Object,
            new Mock<ILogger<PlaceRepository>>().Object);
        var result = await repository.Get(userId);
        
        placeReadContext.Verify(x => x.Places, Times.Once);
        result.Match(succ =>
        {
            Assert.Equal(1, succ.Count);
            return true;
        }, err =>
        {
            Assert.Fail();
            return false;
        });
    }
}