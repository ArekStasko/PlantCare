using Microsoft.EntityFrameworkCore;
using PlantCare.API.DataAccess.Models.Place;

namespace PlantCare.API.DataAccess;

public class PlaceContext : DbContext
{
    public PlaceContext()
    {
    }

    public PlaceContext(DbContextOptions<PlaceContext> options) : base(options)
    {
    }

    public virtual DbSet<Place> Places { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Place>()
            .HasMany(e => e.Plants)
            .WithOne(e => e.Place)
            .HasForeignKey(e => e.PlaceId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }

}