using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.Place;

namespace PlantCare.Persistance.Interfaces;

public interface IPlaceContext
{
    DbSet<Place> Places { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}