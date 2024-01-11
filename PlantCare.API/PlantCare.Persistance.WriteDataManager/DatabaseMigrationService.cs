using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.Persistance.WriteDataManager;

public class DatabaseMigrationService
{
    public static void MigrationInitialization(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        _ = serviceScope ?? throw new NullReferenceException(nameof(serviceScope));
        
        serviceScope.ServiceProvider.GetService<WriteDataContext>()!.Database.Migrate();
    }
}