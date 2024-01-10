using Microsoft.EntityFrameworkCore;
using PlantCare.Persistance.DAO.Place;

namespace PlantCare.Persistance.Interfaces;

public interface IPlaceContext
{
    DbSet<PlaceDAO> Places { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}