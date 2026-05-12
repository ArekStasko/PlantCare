using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.Distributor;

namespace PlantCare.Persistance.ReadDataManager.Interfaces;

public interface IDistributorReadContext
{
    DbSet<Distributor> Distributors { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}