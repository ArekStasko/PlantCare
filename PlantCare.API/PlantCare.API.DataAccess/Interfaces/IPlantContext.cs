using Microsoft.EntityFrameworkCore;
using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.DataAccess.Interfaces;

public interface IPlantContext
{
    DbSet<Plant> Plants { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}