using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.Distributor;

namespace PlantCare.Persistance.WriteDataManager.Interfaces;

public interface IDistributorContext
{
    DbSet<Distributor> Distributors { get; set; }
    DbSet<WaterSupply> WaterSupplies { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}