using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.ReadModels.HumidityMeasurement;

namespace PlantCare.Persistance.ReadDataManager.Interfaces;

public interface IHumidityMeasurementReadContext
{
    DbSet<HumidityMeasurement> HumidityMeasurements { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}