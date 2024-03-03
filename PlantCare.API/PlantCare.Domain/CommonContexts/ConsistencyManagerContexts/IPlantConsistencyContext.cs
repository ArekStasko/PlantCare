using PlantCare.Domain.Models.Plant;

namespace PlantCare.Domain.CommonContexts.ConsistencyManagerContexts;

public interface IPlantConsistencyContext
{
    DbSet<Plant> Plants { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}