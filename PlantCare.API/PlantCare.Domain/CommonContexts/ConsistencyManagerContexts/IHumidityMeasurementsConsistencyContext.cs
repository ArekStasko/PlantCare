using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.HumidityMeasurement;

namespace PlantCare.Domain.CommonContexts.ConsistencyManagerContexts;

public interface IHumidityMeasurementsConsistencyContext
{
    DbSet<HumidityMeasurement> HumidityMeasurements { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}