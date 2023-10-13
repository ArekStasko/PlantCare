using Microsoft.EntityFrameworkCore;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Models.Place;

namespace PlantCare.API.DataAccess;

internal class DataContext : DbContext, IPlaceContext, IPlantContext
{
    public DataContext(){}

    public DataContext(DbContextOptions<DataContext> options) : base(options){}
    
    public virtual DbSet<Plant> Plants { get; set; } = null!;
    public virtual DbSet<Place> Places { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Place>()
            .HasMany(e => e.Plants)
            .WithOne()
            .HasForeignKey(e => e.PlaceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}