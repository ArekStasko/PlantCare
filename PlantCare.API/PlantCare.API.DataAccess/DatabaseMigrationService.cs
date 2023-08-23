using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.API.DataAccess;

public class DatabaseMigrationService
{
    public static void MigrationInitialization(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        serviceScope.ServiceProvider.GetService<DataContext>().Database.Migrate();
    }
}