using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.Module;

namespace PlantCare.Domain.CommonContexts.ConsistencyManagerContexts;

public interface IModuleConsistencyContext
{
    DbSet<Module> Modules { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}