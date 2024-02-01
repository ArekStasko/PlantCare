using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.HumidityMeasurement;

namespace PlantCare.Persistance.Interfaces.ReadContexts;

public interface IHumidityMeasurementReadContext
{
    DbSet<HumidityMeasurement> HumidityMeasurements { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}