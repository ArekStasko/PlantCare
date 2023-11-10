using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using MockQueryable.Moq;
using Moq;
using PlantCare.API.DataAccess.Enums;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models;
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

    public static Mock<IPlantContext> SetupPlantContext()
    {
        IPlant defaultPlant = new Plant()
        {
            Id = 1,
            Name = "Test Name",
            Description = "Test Description",
            Type = PlantType.Fruit,
        };
        
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
}