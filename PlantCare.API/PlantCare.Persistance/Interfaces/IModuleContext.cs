using Microsoft.EntityFrameworkCore;
using PlantCare.Persistance.DAO.Module;

namespace PlantCare.Persistance.Interfaces;

public interface IModuleContext
{
    DbSet<ModuleDAO> Modules { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}