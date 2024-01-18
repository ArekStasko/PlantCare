using Microsoft.EntityFrameworkCore;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.DataAccess.Models.Place;

namespace PlantCare.API.DataAccess;

internal class DataContext : DbContext, IPlaceContext, IPlantContext, IHumidityMeasurementContext, IModuleContext
{
    public DataContext(){}

    public DataContext(DbContextOptions<DataContext> options) : base(options){}

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