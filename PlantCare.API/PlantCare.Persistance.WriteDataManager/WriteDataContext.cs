using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Place;
using PlantCare.Domain.Models.Plant;
using PlantCare.Persistance.WriteDataManager.Interfaces;

namespace PlantCare.Persistance.WriteDataManager;

public class WriteDataContext : DbContext, IPlantWriteContext, IPlaceWriteContext, IModuleWriteContext, IHumidityMeasurementWriteContext
{
    public WriteDataContext(){}

    public WriteDataContext(DbContextOptions<WriteDataContext> options) : base(options){}
    
    public virtual DbSet<Plant> Plants { get; set; } = null!;

    public virtual DbSet<Place> Places { get; set; } = null!;

    public virtual DbSet<Module> Modules { get; set; } = null!;

    public virtual DbSet<HumidityMeasurement> HumidityMeasurements { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var databaseServer = Environment.GetEnvironmentVariable("DatabaseServer");
            var databasePort = Environment.GetEnvironmentVariable("DatabasePort");
            var databaseUser = Environment.GetEnvironmentVariable("DatabaseUser");
            var databasePassword = Environment.GetEnvironmentVariable("DatabasePassword");
            var databaseName = Environment.GetEnvironmentVariable("WriteDatabaseName");

            var connectionString =
                $"Server={databaseServer},{databasePort};Database={databaseName};User Id={databaseUser};Password={databasePassword};TrustServerCertificate=true";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Place>()
            .HasMany(e => e.Plants)
            .WithOne()
            .HasForeignKey(e => e.PlaceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<Module>();

        modelBuilder.Entity<Module>()
            .HasOne(e => e.Plant)
            .WithOne()
            .HasForeignKey<Plant>(e => e.ModuleId)
            .IsRequired();
    }
}