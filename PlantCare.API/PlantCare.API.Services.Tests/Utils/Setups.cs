using AutoMapper;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Repositories.PlantRepository;
using PlantCare.API.Services.Mapper;
using Serilog;

namespace PlantCare.API.Services.Tests.Utils;

public static class Setups
{
    public static IMapper GetMapper()
    {
        var autoMapperProfile = new AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperProfile));
        return new AutoMapper.Mapper(configuration);
    }
    
    public static Mock<PlantRepository> GetSuccessfullPlantRepository()
    {
        var plantContextMock = new Mock<PlantContext>();
        var logger = new Mock<ILogger<PlantRepository>>();
        var mapper = GetMapper();
        
        var plantRepositoryMock = new Mock<PlantRepository>(plantContextMock.Object, logger.Object, mapper);

        plantRepositoryMock.Setup(repo => repo.Create(It.IsAny<IPlant>())).ReturnsAsync(true).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(true).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Edit(It.IsAny<IPlant>())).ReturnsAsync(true).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Get()).ReturnsAsync(It.IsAny<List<IPlant>>()).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(It.IsAny<Result<IPlant>>()).Verifiable();
        
        return plantRepositoryMock;
    }
    
    public static Mock<PlantRepository> GetUnsuccessfullPlantRepository()
    {
        var plantContextMock = new Mock<PlantContext>();
        var logger = new Mock<ILogger<PlantRepository>>();
        var mapper = GetMapper();
        
        var plantRepositoryMock = new Mock<PlantRepository>(plantContextMock.Object, logger.Object, mapper);

        plantRepositoryMock.Setup(repo => repo.Create(It.IsAny<IPlant>())).ReturnsAsync(false).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(false).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Edit(It.IsAny<IPlant>())).ReturnsAsync(false).Verifiable();
        
        return plantRepositoryMock;
    }
}