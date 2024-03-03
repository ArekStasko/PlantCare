using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.Place;

namespace PlantCare.Domain.CommonContexts.ConsistencyManagerContexts;

public interface IPlaceConsistencyContext
{
    DbSet<Place> Places { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}