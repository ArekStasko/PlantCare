using Microsoft.EntityFrameworkCore;
using PlantCare.API.DataAccess.Models.Module;

namespace PlantCare.API.DataAccess.Interfaces;

public interface IModuleContext
{
    DbSet<Module> Modules { get; set; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}