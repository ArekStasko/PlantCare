using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Place;
using PlantCare.Domain.Models.Plant;
using PlantCare.Persistance.Interfaces;

namespace PlantCare.Persistance.WriteDataManager;

public class WriteDataContext : DbContext, IPlantContext, IPlaceContext, IModuleContext, IHumidityMeasurementContext
{
    public WriteDataContext(){}

    public WriteDataContext(DbContextOptions<WriteDataContext> options) : base(options){}
    
    public virtual DbSet<Plant> Plants { get; set; } = null!;

    public virtual DbSet<Place> Places { get; set; } = null!;

    public virtual DbSet<Module> Modules { get; set; } = null!;

    public virtual DbSet<HumidityMeasurement> HumidityMeasurements { get; set; } = null!;

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
    }
}