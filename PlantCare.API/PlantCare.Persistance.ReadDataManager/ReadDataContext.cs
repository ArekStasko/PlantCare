using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Place;
using PlantCare.Domain.Models.Plant;
using PlantCare.Persistance.ReadDataManager.Interfaces;

namespace PlantCare.Persistance.ReadDataManager;

public class ReadDataContext : DbContext, IPlantReadContext, IPlaceReadContext, IModuleReadContext, IHumidityMeasurementReadContext
{
    public ReadDataContext(){}

    public ReadDataContext(DbContextOptions<ReadDataContext> options) : base(options){}
    
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
            var databaseName = Environment.GetEnvironmentVariable("ReadDatabaseName");

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

        modelBuilder.Entity<Module>()
            .HasMany(e => e.HumidityMeasurements)
            .WithOne()
            .HasForeignKey(e => e.ModuleId)
            .IsRequired();

        modelBuilder.Entity<Module>()
            .HasOne(e => e.Plant)
            .WithOne()
            .HasForeignKey<Plant>(e => e.ModuleId)
            .IsRequired();

        modelBuilder.Entity<Place>()
            .Property(p => p.Id)
            .ValueGeneratedNever();
        
        modelBuilder.Entity<Plant>()
            .Property(p => p.Id)
            .ValueGeneratedNever();
        
        modelBuilder.Entity<Module>()
            .Property(p => p.Id)
            .ValueGeneratedNever();
        
        modelBuilder.Entity<HumidityMeasurement>()
            .Property(p => p.Id)
            .ValueGeneratedNever();
    }
}