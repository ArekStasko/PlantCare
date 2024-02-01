using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.Place;

namespace PlantCare.Persistance.Interfaces.ReadContexts;

public interface IPlaceReadContext
{
    DbSet<Place> Places { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}