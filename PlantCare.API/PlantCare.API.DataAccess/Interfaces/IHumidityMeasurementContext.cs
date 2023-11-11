using Microsoft.EntityFrameworkCore;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;

namespace PlantCare.API.DataAccess.Interfaces;

public interface IHumidityMeasurementContext
{
    DbSet<HumidityMeasurement> HumidityMeasurements { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}