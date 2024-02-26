using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.ReadModels.Plant;

namespace PlantCare.Persistance.ReadDataManager.Interfaces;

public interface IPlantReadContext
{
    DbSet<Plant> Plants { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}