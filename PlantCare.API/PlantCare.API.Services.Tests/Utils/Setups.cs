using AutoMapper;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
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

    
    public static Mock<MockPlantRepo> GetSuccessfullPlantRepository()
    {
        var plantRepositoryMock = new Mock<MockPlantRepo>();
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
    
    public static Mock<MockPlantRepo> GetUnsuccessfullPlantRepository()
    {
        var plantRepositoryMock = new Mock<MockPlantRepo>();

        plantRepositoryMock.Setup(repo => repo.Create(It.IsAny<IPlant>())).ReturnsAsync(false).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(false).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Update(It.IsAny<IPlant>())).ReturnsAsync(false).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ThrowsAsync(It.IsAny<ArgumentNullException>()).Verifiable();

        return plantRepositoryMock;
    }
    
    
    public static Mock<MockPlaceRepo> GetSuccessfullPlaceRepository()
    {
        var defaultPlant = new Plant()
        {
            Id = 1,
            Name = "Test Name",
            Description = "Test Description",
            PlaceId = 1,
            Type = 0,
        };
        
        var placeRepositoryMock = new Mock<MockPlaceRepo>();
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
    
    public static Mock<MockPlaceRepo> GetUnsuccessfullPlaceRepository()
    {

        var defaultPlant = new Plant()
        {
            Id = 1,
            Name = "Test Name",
            Description = "Test Description",
            PlaceId = 1,
            Type = 0,
        };
        
        var placeRepositoryMock = new Mock<MockPlaceRepo>();
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