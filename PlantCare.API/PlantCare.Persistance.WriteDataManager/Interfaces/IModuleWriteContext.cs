using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.Module;

namespace PlantCare.Persistance.WriteDataManager.Interfaces;

public interface IModuleWriteContext
{
    DbSet<Module> Modules { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}