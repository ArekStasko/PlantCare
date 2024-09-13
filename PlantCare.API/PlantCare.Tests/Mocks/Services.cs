using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Domain.Models.Module;
using PlantCare.Persistance.WriteDataManager.Interfaces;
using PlantCare.Persistance.WriteDataManager.Repositories;

namespace PlantCare.Tests.Mocks;

public static class Services
{
    public static Mock<DbSet<HumidityMeasurement>> humidityMeasurementsDb { get; } = new();
    public static Mock<DbSet<Module>> moduleDb { get; } = new ();
    public static Mock<IHumidityMeasurementWriteContext> HumidityMeasurementWriteContext { get; } = new();
    public static Mock<IModuleWriteContext> ModuleWriteContext { get; } = new();

    public static DbSet<Module> GenerateModule()
    {
        var module = new Module()
        {
            Id = new Guid("6ac2713b-ecb3-41fe-b8db-e72ca5621209"),
            UserId = 1
        };
        DbSet<Module> record = null;
        record.Add(module);
    }
}