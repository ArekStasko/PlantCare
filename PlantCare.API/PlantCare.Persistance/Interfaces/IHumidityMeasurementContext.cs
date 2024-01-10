using Microsoft.EntityFrameworkCore;
using PlantCare.Persistance.DAO.HumidityMeasurement;

namespace PlantCare.Persistance.Interfaces;

public interface IHumidityMeasurementContext
{
    DbSet<HumidityMeasurementDAO> HumidityMeasurements { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}