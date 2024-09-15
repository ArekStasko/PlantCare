using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Persistance.ReadDataManager.Repositories;
using PlantCare.Tests.Mocks;

namespace PlantCare.Tests.ReadDataManager;

public class ModuleRepositoryTests
{
    [Fact]
    public async void GetModulesTest()
    {
        var userId = 1;
        var moduleDb = Services.moduleDb;
        var moduleReadContext = Services.ModuleReadContext();
        moduleReadContext.Setup(x => x.Modules).Returns(moduleDb.Object);

        var repository = new ModuleRepository(moduleReadContext.Object,
            new Mock<ILogger<ModuleRepository>>().Object);
        var result = await repository.Get(userId);
        
        moduleReadContext.Verify(x => x.Modules, Times.Once);
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