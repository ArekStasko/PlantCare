using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using MockQueryable.Moq;
using Moq;
using PlantCare.API.DataAccess.Enums;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.DataAccess.Models.Place;
using PlantCare.API.Services.Mapper;

namespace PlantCare.API.DataAccess.Tests.Utils;

public class Setups
{
    public static IMapper SetupMapper()
    {
        var autoMapperProfile = new AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperProfile));
        return new Mapper(configuration);
    }

    public static IDistributedCache SetupCache()
    {
        IDistributedCache cache = new MockCache();
        return cache;
    }
    
    public static Mock<IModuleContext> SetupModuleContext()
    {
        var moduleContextMock = new Mock<IModuleContext>();
        
        moduleContextMock.Setup(_ => _.Modules.Remove(It.IsAny<Module>())).Returns((EntityEntry<Module>)null).Verifiable();
        
        moduleContextMock.Setup(_ => _.Modules.AddAsync(It.IsAny<Module>(), It.IsAny<CancellationToken>()))
            .Returns(ValueTask.FromResult((EntityEntry<Module>)null)).Verifiable();

        return moduleContextMock;
    }
    
    public static Mock<IHumidityMeasurementContext> SetupHumidityMeasurementsContext()
    {
        var humidityMeasurementContextMock = new Mock<IHumidityMeasurementContext>();
        
        humidityMeasurementContextMock.Setup(_ => _.HumidityMeasurements.AddAsync(It.IsAny<HumidityMeasurement>(), It.IsAny<CancellationToken>()))
            .Returns(ValueTask.FromResult((EntityEntry<HumidityMeasurement>)null)).Verifiable();

        return humidityMeasurementContextMock;
    }

    public static Mock<IPlantContext> SetupPlantContext()
    {
        var plantContextMock = new Mock<IPlantContext>();
        
        plantContextMock.Setup(_ => _.Plants.Remove(It.IsAny<Plant>())).Returns((EntityEntry<Plant>)null).Verifiable();
        
        plantContextMock.Setup(_ => _.Plants.AddAsync(It.IsAny<Plant>(), It.IsAny<CancellationToken>()))
            .Returns(ValueTask.FromResult((EntityEntry<Plant>)null)).Verifiable();

        return plantContextMock;
    }

    public static Mock<DbSet<Plant>> GetPlantMockData()
    {
        var data = new List<Plant>()
        {
            new()
            {
                Id = 1,
                Name = "Test Name",
                Description = "Test Description",
                PlaceId = 1,
                Type = PlantType.Fruit,
            },
            new()
            {
                Id = 2,
                Name = "Test Name",
                Description = "Test Description",
                PlaceId = 1,
                Type = PlantType.Fruit,
            },
            new()
            {
                Id = 3,
                Name = "Test Name",
                Description = "Test Description",
                PlaceId = 1,
                Type = PlantType.Fruit,
            },
            new()
            {
                Id = 4,
                Name = "Test Name",
                Description = "Test Description",
                PlaceId = 1,
                Type = PlantType.Fruit,
            },
            new()
            {
                Id = 5,
                Name = "Test Name",
                Description = "Test Description",
                PlaceId = 1,
                Type = PlantType.Fruit,
            },
            new()
            {
                Id = 6,
                Name = "Test Name",
                Description = "Test Description",
                PlaceId = 1,
                Type = PlantType.Fruit,
            },
            new()
            {
                Id = 7,
                Name = "Test Name",
                Description = "Test Description",
                PlaceId = 1,
                Type = PlantType.Fruit,
            },
            new()
            {
                Id = 8,
                Name = "Test Name",
                Description = "Test Description",
                PlaceId = 1,
                Type = PlantType.Fruit,
            },
            new()
            {
                Id = 9,
                Name = "Test Name",
                Description = "Test Description",
                PlaceId = 1,
                Type = PlantType.Fruit,
            },
            new()
            {
                Id = 10,
                Name = "Test Name",
                Description = "Test Description",
                PlaceId = 1,
                Type = PlantType.Fruit,
            }
        }.AsQueryable().BuildMockDbSet();

        return data;
    }
    
    public static Mock<IPlaceContext> SetupPlaceContext()
    {
        var placeContextMock = new Mock<IPlaceContext>();
        
        placeContextMock.Setup(_ => _.Places.Remove(It.IsAny<Place>())).Returns((EntityEntry<Place>)null).Verifiable();
        
        placeContextMock.Setup(_ => _.Places.AddAsync(It.IsAny<Place>(), It.IsAny<CancellationToken>()))
            .Returns(ValueTask.FromResult((EntityEntry<Place>)null)).Verifiable();

        return placeContextMock;
    }
    
    public static Mock<DbSet<Place>> GetPlaceMockData()
    {
        
        Plant defaultPlant = new Plant()
        {
            Id = 1,
            Name = "Test Name",
            Description = "Test Description",
            PlaceId = 1,
            Type = PlantType.Fruit,
        };
        
        var data = new List<Place>()
        {
            new()
            {
                Id = 1,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new()
            {
                Id = 2,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new()
            {
                Id = 3,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new()
            {
                Id = 4,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new()
            {
                Id = 5,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new()
            {
                Id = 6,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new()
            {
                Id = 7,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new()
            {
                Id = 8,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new()
            {
                Id = 9,
                Name = "Test Name",
                Plants = { defaultPlant }
            },
            new()
            {
                Id = 10,
                Name = "Test Name",
                Plants = { defaultPlant }
            }
        }.AsQueryable().BuildMockDbSet();

        return data;
    }
    public static Mock<DbSet<HumidityMeasurement>> GetHumidityMeasurementMockData()
    {
        var data = new List<HumidityMeasurement>()
        {
            new()
            {
                Id = 1,
                Humidity = 70,
            },
            new()
            {
                Id = 2,
                Humidity = 70,
            },
            new()
            {
                Id = 3,
                Humidity = 70,
            },
            new()
            {
                Id = 4,
                Humidity = 70,
            },
            new()
            {
                Id = 5,
                Humidity = 70,
            },
            new()
            {
                Id = 6,
                Humidity = 70,
            },
            new()
            {
                Id = 7,
                Humidity = 70,
            },
            new()
            {
                Id = 8,
                Humidity = 70,
            },
            new()
            {
                Id = 9,
                Humidity = 70,
            },
            new()
            {
                Id = 10,
                Humidity = 70,
            }
        }.AsQueryable().BuildMockDbSet();

        return data;
    }
    public static Mock<DbSet<Module>> GetModuleMockData()
    {
        var data = new List<Module>()
        {
            new()
            {
                Id = Guid.Parse("22e44148-84ae-4e2f-b698-ae0cea661313"),
            },
            new()
            {
                Id = new Guid(),
            },
            new()
            {
                Id = new Guid(),
            },
            new()
            {
                Id = new Guid(),
            },
            new()
            {
                Id = new Guid(),
            },
            new()
            {
                Id = new Guid(),
            },
            new()
            {
                Id = new Guid(),
            },
            new()
            {
                Id = new Guid(),
            },
            new()
            {
                Id = new Guid(),
            },
            new()
            {
                Id = new Guid(),
            }
        }.AsQueryable().BuildMockDbSet();

        return data;
    }
}