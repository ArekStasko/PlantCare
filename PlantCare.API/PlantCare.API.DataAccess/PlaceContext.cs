using Microsoft.EntityFrameworkCore;
using PlantCare.API.DataAccess.Models.Place;

namespace PlantCare.API.DataAccess;

public class PlaceContext : DbContext
{
    public PlaceContext(){}

    public PlaceContext(DbContextOptions<PlaceContext> options) : base(options){}

    public virtual DbSet<Place> Places { get; set; } = null!;
}