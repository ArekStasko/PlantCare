using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Domain.Models.Module;
using PlantCare.Persistance.WriteDataManager.Interfaces;
using PlantCare.Persistance.WriteDataManager.Repositories;
using PlantCare.Tests.Mocks;

namespace PlantCare.Tests.WriteDataManager;

public class ModuleRepositoryTests
{

    [Fact]
    public async void AddModuleTest()
    {
        var userId = 1;
        var moduleId = Guid.NewGuid();
        Mock<DbSet<Module>> moduleDb = Services.moduleDb;
        Mock<IModuleWriteContext> moduleWriteContext = Services.ModuleWriteContext;
        
        moduleWriteContext.Setup(x => x.Modules).Returns(moduleDb.Object);
        moduleWriteContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));

        var moduleRepo = new ModuleRepository(moduleWriteContext.Object, new Mock<ILogger<ModuleRepository>>().Object);
        var result = await moduleRepo.Add(userId, moduleId);
        
        moduleDb.Verify(x => x.AddAsync(It.IsAny<Module>(), It.IsAny<CancellationToken>()), Times.Once());
        moduleWriteContext.Verify(x => x.Modules, Times.Once());
        moduleWriteContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        result.Match(succ =>
        {
            Assert.Equal(moduleId, succ);
            return true;
        }, err =>
        {
            Assert.Fail();
            return false;
        });
    }

    [Fact]
    public async void DeleteModuleTest()
    {
        var userId = 1;
        var moduleId = new Guid("6ac2713b-ecb3-41fe-b8db-e72ca5621209");
        Mock<DbSet<Module>> moduleDb = Services.moduleDb;
        Mock<IModuleWriteContext> moduleWriteContext = Services.ModuleWriteContext;
        moduleWriteContext.Setup(x => x.Modules).Returns(moduleDb.Object);
        moduleWriteContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        moduleDb.Setup(x => x.Remove(It.IsAny<Module>()));

        var moduleRepo = new ModuleRepository(moduleWriteContext.Object, new Mock<ILogger<ModuleRepository>>().Object);
        var result = await moduleRepo.Delete(userId, moduleId);
        
        moduleDb.Verify(x => x.Remove(It.IsAny<Module>()), Times.Once);
        moduleWriteContext.Verify(x => x.Modules, Times.AtLeast(2));
        moduleWriteContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        
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
    public async void UpdateModuleTest()
    {
        var moduleToUpdate = new Module()
        {
            UserId = 1,
            Id = new Guid("6ac2713b-ecb3-41fe-b8db-e72ca5621209")
        };
        Mock<DbSet<Module>> moduleDb = Services.moduleDb;
        Mock<IModuleWriteContext> moduleWriteContext = Services.ModuleWriteContext;
        
        moduleDb.Setup(x => x.AddAsync(It.IsAny<Module>(), It.IsAny<CancellationToken>()));
        moduleWriteContext.Setup(x => x.Modules).Returns(moduleDb.Object);
        moduleWriteContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));

        var moduleRepo = new ModuleRepository(moduleWriteContext.Object, new Mock<ILogger<ModuleRepository>>().Object);
        var result = await moduleRepo.Update(moduleToUpdate);
        
        moduleWriteContext.Verify(x => x.Modules, Times.Once());
        moduleWriteContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
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