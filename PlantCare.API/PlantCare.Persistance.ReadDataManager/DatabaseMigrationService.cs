using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.Persistance.ReadDataManager;

public class DatabaseMigrationService
{
    public static void MigrationInitialization(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        _ = serviceScope ?? throw new NullReferenceException(nameof(serviceScope));
        
        serviceScope.ServiceProvider.GetService<ReadDataContext>()!.Database.Migrate();
    }
}