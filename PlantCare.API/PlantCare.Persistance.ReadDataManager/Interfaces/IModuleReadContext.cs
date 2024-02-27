using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.Module;

namespace PlantCare.Persistance.ReadDataManager.Interfaces;

public interface IModuleReadContext
{
    DbSet<Module> Modules { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}