using AutoMapper;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Models.Place;
using PlantCare.API.DataAccess.Repositories.PlaceRepository;
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
        var plantContextMock = new Mock<IPlantContext>();
        var logger = new Mock<ILogger<PlantRepository>>();
        var mapper = GetMapper();
        
        var plantRepositoryMock = new Mock<PlantRepository>(plantContextMock.Object, logger.Object, mapper);
        var plantList = new List<IPlant>()
        {
            new Plant()
            {
                Name = "Test Name",
                Description = "Test Description",
                Type = 0,
            },
            new Plant()
            {
                Name = "Test Name",
                Description = "Test Description",
                Type = 0,
            },
            new Plant()
            {
                Name = "Test Name",
                Description = "Test Description",
                Type = 0,
            }
        };

        var plant = new Plant()
        {
            Name = "Test Name",
            Description = "Test Description",
            Type = 0,
        };
        
        plantRepositoryMock.Setup(repo => repo.Create(It.IsAny<IPlant>())).ReturnsAsync(true).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(true).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Update(It.IsAny<IPlant>())).ReturnsAsync(true).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Get()).ReturnsAsync(plantList).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(plant).Verifiable();
        
        return plantRepositoryMock;
    }
    
    public static Mock<PlantRepository> GetUnsuccessfullPlantRepository()
    {
        var plantContextMock = new Mock<IPlantContext>();
        var logger = new Mock<ILogger<PlantRepository>>();
        var mapper = GetMapper();
        
        var plantRepositoryMock = new Mock<PlantRepository>(plantContextMock.Object, logger.Object, mapper);

        plantRepositoryMock.Setup(repo => repo.Create(It.IsAny<IPlant>())).ReturnsAsync(false).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(false).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Update(It.IsAny<IPlant>())).ReturnsAsync(false).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ThrowsAsync(It.IsAny<ArgumentNullException>()).Verifiable();

        return plantRepositoryMock;
    }
    
    
    public static Mock<PlaceRepository> GetSuccessfullPlaceRepository()
    {
        var placeContextMock = new Mock<IPlaceContext>();
        var logger = new Mock<ILogger<IPlaceContext>>();
        var mapper = GetMapper();

        var defaultPlant = new Plant()
        {
            Id = 1,
            Name = "Test Name",
            Description = "Test Description",
            PlaceId = 1,
            Type = 0,
        };
        
        var placeRepositoryMock = new Mock<PlaceRepository>(placeContextMock.Object, logger.Object, mapper);
        var placeList = new List<IPlace>()
        {
            new Place()
            {
                Id = 1,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new Place()
            {
                Id = 2,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new Place()
            {
                Id = 3,
                Name = "Test Name",
                Plants = { defaultPlant }
            }
        };
        
        placeRepositoryMock.Setup(repo => repo.Create(It.IsAny<IPlace>())).ReturnsAsync(true).Verifiable();
        placeRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(true).Verifiable();
        placeRepositoryMock.Setup(repo => repo.Update(It.IsAny<IPlace>())).ReturnsAsync(true).Verifiable();
        placeRepositoryMock.Setup(repo => repo.Get()).ReturnsAsync(placeList).Verifiable();
        
        return placeRepositoryMock;
    }
    
    public static Mock<PlaceRepository> GetUnsuccessfullPlaceRepository()
    {
        var placeContextMock = new Mock<IPlaceContext>();
        var logger = new Mock<ILogger<IPlaceContext>>();
        var mapper = GetMapper();

        var defaultPlant = new Plant()
        {
            Id = 1,
            Name = "Test Name",
            Description = "Test Description",
            PlaceId = 1,
            Type = 0,
        };
        
        var placeRepositoryMock = new Mock<PlaceRepository>(placeContextMock.Object, logger.Object, mapper);
        var placeList = new List<IPlace>()
        {
            new Place()
            {
                Id = 1,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new Place()
            {
                Id = 2,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new Place()
            {
                Id = 3,
                Name = "Test Name",
                Plants = { defaultPlant }
            }
        };
        
        //TODO To make it work i had to make my repo methods virtual, is it a good practice ? 
        
        placeRepositoryMock.Setup(repo => repo.Create(It.IsAny<IPlace>())).ReturnsAsync(false).Verifiable();
        placeRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(false).Verifiable();
        placeRepositoryMock.Setup(repo => repo.Update(It.IsAny<IPlace>())).ReturnsAsync(false).Verifiable();
        placeRepositoryMock.Setup(repo => repo.Get()).ReturnsAsync(placeList).Verifiable();
        
        return placeRepositoryMock;
    }
}