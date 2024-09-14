using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Persistance.ReadDataManager.Repositories;
using PlantCare.Tests.Mocks;

namespace PlantCare.Tests.ReadDataManager;

public class PlantRepositoryTests
{
    [Fact]
    public async void GetPlantsTest()
    {
        var userId = 1;
        var plantDb = Services.PlantDb;
        var plantReadContext = Services.PlantReadContext();
        plantReadContext.Setup(x => x.Plants).Returns(plantDb.Object);

        var repository = new PlantRepository(plantReadContext.Object,
            new Mock<ILogger<PlantRepository>>().Object);
        var result = await repository.Get(userId);
        
        plantReadContext.Verify(x => x.Plants, Times.Once);
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