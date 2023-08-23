using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PlantCare.API.DataAccess;

public static class DataExtensions
{
    public static void AddDataContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    public static void Migrate(this IApplicationBuilder app) => DatabaseMigrationService.MigrationInitialization(app);
}