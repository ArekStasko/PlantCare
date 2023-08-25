using Microsoft.EntityFrameworkCore;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Models.Record;

namespace PlantCare.API.DataAccess;

public class DataContext : DbContext
{
    public DataContext(){}

    public DataContext(DbContextOptions options) : base(options){}
    
    public virtual DbSet<Plant> Plants { get; set; } = null!;
    public virtual DbSet<Record> Records { get; set; } = null!;
}