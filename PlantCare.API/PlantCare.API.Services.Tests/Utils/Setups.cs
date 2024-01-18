using System.Numerics;
using AutoMapper;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.DataAccess.Models.Module;
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
    
    public static Mock<MockHumidityMeasurementsRepo> GetSuccessfullHumidityMeasurementsRepository()
    {
        var HumidityMeasurementsRepositoryMock = new Mock<MockHumidityMeasurementsRepo>();
        var HumidityMeasurementsList = new List<IHumidityMeasurement>()
        {
            new HumidityMeasurement()
            {
                Id = 1,
                ModuleId = new Guid(),
                Humidity = 90,
                MeasurementDate = DateTime.Now
            },
            new HumidityMeasurement()
            {
                Id = 2,
                ModuleId = new Guid(),
                Humidity = 90,
                MeasurementDate = DateTime.Now
            },
            new HumidityMeasurement()
            {
                Id = 3,
                ModuleId = new Guid(),
                Humidity = 90,
                MeasurementDate = DateTime.Now
            }
        };
        
        HumidityMeasurementsRepositoryMock.Setup(repo => repo.Add(It.IsAny<IHumidityMeasurement>())).ReturnsAsync(new Result<bool>(true)).Verifiable();
        HumidityMeasurementsRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync(HumidityMeasurementsList).Verifiable();
        
        return HumidityMeasurementsRepositoryMock;
    }
    
    public static Mock<MockHumidityMeasurementsRepo> GetUnsuccessfullHumidityMeasurementsRepository()
    {
        var HumidityMeasurementsRepositoryMock = new Mock<MockHumidityMeasurementsRepo>();

        HumidityMeasurementsRepositoryMock.Setup(repo => repo.Add(It.IsAny<IHumidityMeasurement>())).ReturnsAsync(new Result<bool>(false)).Verifiable();
        HumidityMeasurementsRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync(new Result<IReadOnlyCollection<IHumidityMeasurement>>(new Exception())).Verifiable();

        return HumidityMeasurementsRepositoryMock;
    }

    public static Mock<MockModuleRepo> GetSuccessfullModuleRepository()
    {
        var moduleRepositoryMock = new Mock<MockModuleRepo>();
        var modulesList = new List<IModule>()
        {
            new Module()
            {
                Id = new Guid(),
                RequiredMoistureLevel = 80,
                CriticalMoistureLevel = 60,
                HumidityMeasurements = new List<HumidityMeasurement>(){},
                Plant = new Plant(){}
            },
            new Module()
            {
                Id = new Guid(),
                RequiredMoistureLevel = 90,
                CriticalMoistureLevel = 70,
                HumidityMeasurements = new List<HumidityMeasurement>(){},
                Plant = new Plant(){}
            },
            new Module()
            {
                Id = new Guid(),
                RequiredMoistureLevel = 70,
                CriticalMoistureLevel = 50,
                HumidityMeasurements = new List<HumidityMeasurement>(){},
                Plant = new Plant(){}
            }
        };

        var module = new Module()
        {
            Id = new Guid(),
            RequiredMoistureLevel = 80,
            CriticalMoistureLevel = 60,
            HumidityMeasurements = new List<HumidityMeasurement>(){},
            Plant = new Plant(){}
        };
        
        moduleRepositoryMock.Setup(repo => repo.Add(It.IsAny<Guid>())).ReturnsAsync(new Result<Guid>()).Verifiable();
        moduleRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Guid>())).ReturnsAsync(true).Verifiable();
        moduleRepositoryMock.Setup(repo => repo.Update(It.IsAny<IModule>())).ReturnsAsync(true).Verifiable();
        moduleRepositoryMock.Setup(repo => repo.Get()).ReturnsAsync(modulesList).Verifiable();
        moduleRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ReturnsAsync(module).Verifiable();
        
        return moduleRepositoryMock;
    }
    
    public static Mock<MockModuleRepo> GetUnsuccessfullModuleRepository()
    {
        var plantRepositoryMock = new Mock<MockModuleRepo>();

        plantRepositoryMock.Setup(repo => repo.Add(It.IsAny<Guid>())).ReturnsAsync(new Result<Guid>(new Exception())).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Guid>())).ReturnsAsync(false).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Update(It.IsAny<IModule>())).ReturnsAsync(false).Verifiable();
        plantRepositoryMock.Setup(repo => repo.Get(It.IsAny<Guid>())).ThrowsAsync(It.IsAny<ArgumentNullException>()).Verifiable();

        return plantRepositoryMock;
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