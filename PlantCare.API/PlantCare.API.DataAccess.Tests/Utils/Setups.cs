using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using MockQueryable.Moq;
using Moq;
using PlantCare.API.DataAccess.Enums;
using PlantCare.API.DataAccess.Models;
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

    public static Mock<PlantContext> SetupDataContext()
    {
        IPlant defaultPlant = new Plant()
        {
            Id = 1,
            Name = "Test Name",
            Description = "Test Description",
            Type = PlantType.Fruit,
            CriticalMoistureLevel = 30,
            RequiredMoistureLevel = 70,
            ModuleId = Guid.NewGuid()
        };
        
        var dataContextMock = new Mock<PlantContext>();
        
        dataContextMock.Setup(_ => _.Plants.Remove(It.IsAny<Plant>())).Returns((EntityEntry<Plant>)null).Verifiable();
        
        dataContextMock.Setup(_ => _.Plants.AddAsync(It.IsAny<Plant>(), It.IsAny<CancellationToken>()))
            .Returns(ValueTask.FromResult((EntityEntry<Plant>)null)).Verifiable();

        return dataContextMock;
    }

    public static Mock<DbSet<Plant>> GetMockData()
    {
        var data = new List<Plant>()
        {
            new()
            {
                Id = 1,
                Name = "Test Name",
                Description = "Test Description",
                Type = PlantType.Fruit,
                CriticalMoistureLevel = 30,
                RequiredMoistureLevel = 70,
                ModuleId = Guid.NewGuid()
            },
            new()
            {
                Id = 2,
                Name = "Test Name",
                Description = "Test Description",
                Type = PlantType.Fruit,
                CriticalMoistureLevel = 30,
                RequiredMoistureLevel = 70,
                ModuleId = Guid.NewGuid()
            },
            new()
            {
                Id = 3,
                Name = "Test Name",
                Description = "Test Description",
                Type = PlantType.Fruit,
                CriticalMoistureLevel = 30,
                RequiredMoistureLevel = 70,
                ModuleId = Guid.NewGuid()
            },
            new()
            {
                Id = 4,
                Name = "Test Name",
                Description = "Test Description",
                Type = PlantType.Fruit,
                CriticalMoistureLevel = 30,
                RequiredMoistureLevel = 70,
                ModuleId = Guid.NewGuid()
            },
            new()
            {
                Id = 5,
                Name = "Test Name",
                Description = "Test Description",
                Type = PlantType.Fruit,
                CriticalMoistureLevel = 30,
                RequiredMoistureLevel = 70,
                ModuleId = Guid.NewGuid()
            },
            new()
            {
                Id = 6,
                Name = "Test Name",
                Description = "Test Description",
                Type = PlantType.Fruit,
                CriticalMoistureLevel = 30,
                RequiredMoistureLevel = 70,
                ModuleId = Guid.NewGuid()
            },
            new()
            {
                Id = 7,
                Name = "Test Name",
                Description = "Test Description",
                Type = PlantType.Fruit,
                CriticalMoistureLevel = 30,
                RequiredMoistureLevel = 70,
                ModuleId = Guid.NewGuid()
            },
            new()
            {
                Id = 8,
                Name = "Test Name",
                Description = "Test Description",
                Type = PlantType.Fruit,
                CriticalMoistureLevel = 30,
                RequiredMoistureLevel = 70,
                ModuleId = Guid.NewGuid()
            },
            new()
            {
                Id = 9,
                Name = "Test Name",
                Description = "Test Description",
                Type = PlantType.Fruit,
                CriticalMoistureLevel = 30,
                RequiredMoistureLevel = 70,
                ModuleId = Guid.NewGuid()
            },
            new()
            {
                Id = 10,
                Name = "Test Name",
                Description = "Test Description",
                Type = PlantType.Fruit,
                CriticalMoistureLevel = 30,
                RequiredMoistureLevel = 70,
                ModuleId = Guid.NewGuid()
            }
        }.AsQueryable().BuildMockDbSet();

        return data;
    }
}