using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using PlantCare.Persistance.WriteDataManager.Interfaces;
using MockQueryable.Moq;
using PlantCare.Commands.MapperProfile;
using PlantCare.Domain.Enums;
using PlantCare.Domain.Models.Place;
using PlantCare.MessageBroker.Producer;
using PlantCare.Persistance.ReadDataManager.Interfaces;
using PlantCare.Persistance.WriteDataManager.Repositories.Interfaces;
using HumidityMeasurement = PlantCare.Domain.Models.HumidityMeasurement.HumidityMeasurement;
using Module = PlantCare.Domain.Models.Module.Module;
using Plant = PlantCare.Domain.Models.Plant.Plant;
using MessagePlant = PlantCare.MessageBroker.Messages.Plant;

namespace PlantCare.Tests.Mocks;

public static class Services
{
    public static Mock<DbSet<HumidityMeasurement>> humidityMeasurementsDb { get; } = new List<HumidityMeasurement>()
    {
        new()
        {
            Humidity = 53,
            Id = 1,
            MeasurementDate = DateTime.Now,
            ModuleId = 1,
        }
    }.AsQueryable().BuildMockDbSet();
    public static Mock<DbSet<Module>> moduleDb { get; } = new List<Module>()
    {
        new()
        {
            Id = 1,
            UserId = 1
        }
    }.AsQueryable().BuildMockDbSet();

    public static Mock<DbSet<Plant>> PlantDb { get; } = new List<Plant>()
    {
        new()
        {
            Id = 1,
            UserId = 1,
            Description = "",
            ModuleId = 1,
            PlaceId = 1, 
            Type = PlantType.Decorative
        }
    }.AsQueryable().BuildMockDbSet();

    public static Mock<DbSet<Place>> PlaceDb { get; } = new List<Place>()
    {
        new()
        {
            Id = 1,
            UserId = 1,
            Name = ""
        }
    }.AsQueryable().BuildMockDbSet();
    
    public static Mock<IHumidityMeasurementWriteContext> HumidityMeasurementWriteContext() => new();
    public static Mock<IModuleWriteContext> ModuleWriteContext() => new();

    public static Mock<IPlantWriteContext> PlantWriteContext() => new ();
    public static Mock<IPlaceWriteContext> PlaceWriteContext() => new();
    public static Mock<IHumidityMeasurementReadContext> HumidityMeasurementReadContext() => new();
    public static Mock<IModuleReadContext> ModuleReadContext() => new();
    public static Mock<IPlaceReadContext> PlaceReadContext() => new();
    public static Mock<IPlantReadContext> PlantReadContext() => new();
    public static Mock<IWritePlantRepository> PlantWriteRepository() => new();
    public static Mock<IQueueProducer<MessagePlant>> PlantQueueProducer() => new();
    public static Mock<IWriteHumidityMeasurementRepository> HumidityMeasurementRepository() => new();
    public static Mock<IQueueProducer<MessageBroker.Messages.HumidityMeasurement>> HumidityQueueProducer() => new();
    public static Mock<IWritePlaceRepository> PlaceWriteRepository() => new();
    public static Mock<IQueueProducer<MessageBroker.Messages.Place>> PlaceQueueProducer() => new();
    public static Mock<IWriteModuleRepository> ModuleWriteRepository() => new();
    public static Mock<IQueueProducer<MessageBroker.Messages.Module>> ModuleQueueProducer() => new();
    public static IMapper CommandsMapper => new Mapper(new MapperConfiguration(x => x.AddProfile(new CommandsMapperProfile())));
}