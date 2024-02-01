using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.Plant;

namespace PlantCare.Persistance.Interfaces.ReadContexts;

public interface IPlantReadContext
{
    DbSet<Plant> Plants { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}