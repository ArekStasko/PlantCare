using Microsoft.EntityFrameworkCore;
using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.DataAccess;

public class DataContext : DbContext
{
    public DataContext(){}

    public DataContext(DbContextOptions options) : base(options){}
    
    public virtual DbSet<Plant> Plants { get; set; } = null!;
}