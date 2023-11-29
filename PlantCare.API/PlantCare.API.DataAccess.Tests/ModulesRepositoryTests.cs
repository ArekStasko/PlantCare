using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess.Enums;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.DataAccess.Repositories.ModuleRepository;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.DataAccess.Tests.Utils;

namespace PlantCare.API.DataAccess.Tests;

public class ModulesRepositoryTests
{
    private Mock<IModuleContext> _moduleContextMock = Setups.SetupModuleContext();
    private IMock<ILogger<ModuleRepository>> _loggerMock = new Mock<ILogger<ModuleRepository>>();
    private IDistributedCache _cache = Setups.SetupCache();

    [Test]
    public async Task Create_Should_CreateOnePlant()
    {
        var moduleRepository = new ModuleRepository(_moduleContextMock.Object, _loggerMock.Object, _cache);
        var result = await moduleRepository.Add(new Guid());
        
        _moduleContextMock.Verify(dbSet => dbSet.Modules.AddAsync(It.IsAny<Module>(), It.IsAny<CancellationToken>()), Times.Once);
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
    public async Task Delete_Should_DeleteModule()
    {
        var mockSet = Setups.GetModuleMockData();
        _moduleContextMock.Setup(_ => _.Modules).Returns(mockSet.Object);
        
        var moduleRepository = new ModuleRepository(_moduleContextMock.Object, _loggerMock.Object, _cache);
        var result = await moduleRepository.Delete(Guid.Parse("22e44148-84ae-4e2f-b698-ae0cea661313"));
        
        _moduleContextMock.Verify(dbSet => dbSet.Modules.Remove(It.IsAny<Module>()), Times.Once);
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
        IModule UpdatedModule = new Module()
        { 
            Id = Guid.Parse("22e44148-84ae-4e2f-b698-ae0cea661313"),
            CriticalMoistureLevel = 80,
            RequiredMoistureLevel = 90,
        };

        var mockSet = Setups.GetModuleMockData();
        _moduleContextMock.Setup(_ => _.Modules).Returns(mockSet.Object);

        var moduleRepository = new ModuleRepository(_moduleContextMock.Object, _loggerMock.Object, _cache);
        var result = await moduleRepository.Update(UpdatedModule);
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
        var mockSet = Setups.GetModuleMockData();
        _moduleContextMock.Setup(_ => _.Modules).Returns(mockSet.Object);

        var moduleRepository = new ModuleRepository(_moduleContextMock.Object, _loggerMock.Object, _cache);
        var result = await moduleRepository.Get();
        result.Match<IActionResult>(succ =>
        {
            Assert.IsNotNull(succ);
            return new EmptyResult();
        }, err =>
        {
            Assert.IsNotNull(err);
            return new EmptyResult();
        });
    }
    
    [Test]
    public async Task GetById_Should_ReturnPlant()
    {
        var mockSet = Setups.GetModuleMockData();
        _moduleContextMock.Setup(_ => _.Modules).Returns(mockSet.Object);

        var moduleRepository = new ModuleRepository(_moduleContextMock.Object, _loggerMock.Object, _cache);
        var result = await moduleRepository.Get(Guid.Parse("22e44148-84ae-4e2f-b698-ae0cea661313"));
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