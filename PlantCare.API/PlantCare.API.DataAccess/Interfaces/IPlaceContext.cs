using Microsoft.EntityFrameworkCore;
using PlantCare.API.DataAccess.Models.Place;

namespace PlantCare.API.DataAccess.Interfaces;

public interface IPlaceContext
{ 
    DbSet<Place> Places { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}