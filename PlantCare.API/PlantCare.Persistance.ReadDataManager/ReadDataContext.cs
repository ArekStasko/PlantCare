using Microsoft.EntityFrameworkCore;
using PlantCare.Persistance.DAO.HumidityMeasurement;
using PlantCare.Persistance.DAO.Module;
using PlantCare.Persistance.DAO.Place;
using PlantCare.Persistance.DAO.Plant;

namespace PlantCare.Persistance.ReadDataManager;

public class ReadDataContext : DbContext
{
    public ReadDataContext(){}

    public ReadDataContext(DbContextOptions<ReadDataContext> options) : base(options){}
    
    public virtual DbSet<PlantDAO> Plants { get; set; } = null!;

    public virtual DbSet<PlaceDAO> Places { get; set; } = null!;

    public virtual DbSet<ModuleDAO> Modules { get; set; } = null!;

    public virtual DbSet<HumidityMeasurementDAO> HumidityMeasurements { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlaceDAO>()
            .HasMany(e => e.Plants)
            .WithOne()
            .HasForeignKey(e => e.PlaceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<ModuleDAO>()
            .HasMany(e => e.HumidityMeasurements)
            .WithOne()
            .HasForeignKey(e => e.ModuleId)
            .IsRequired();

        modelBuilder.Entity<ModuleDAO>()
            .HasOne(e => e.Plant)
            .WithOne()
            .HasForeignKey<PlantDAO>(e => e.ModuleId)
            .IsRequired();
    }
}