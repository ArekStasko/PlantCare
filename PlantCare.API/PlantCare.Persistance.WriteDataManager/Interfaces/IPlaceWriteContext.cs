using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.Place;

namespace PlantCare.Persistance.WriteDataManager.Interfaces;

public interface IPlaceWriteContext
{
    DbSet<Place> Places { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}