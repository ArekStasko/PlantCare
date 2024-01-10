using Microsoft.EntityFrameworkCore;
using PlantCare.Persistance.DAO.Plant;

namespace PlantCare.Persistance.Interfaces;

public interface IPlantContext
{
    DbSet<PlantDAO> Plants { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}