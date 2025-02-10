using FieldsService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldsService.Data;

public class FieldsDbContext(DbContextOptions<FieldsDbContext> options) : DbContext(options)
{
    public DbSet<Field> Fields { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Field>()
            .HasIndex(f => new { f.FarmId, FieldName = f.Name })
            .IsUnique();
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasPostgresExtension("postgis");
        
        modelBuilder.Entity<Field>()
            .Property(f => f.Boundary)
            .HasColumnType("geometry(Polygon, 4326)");
    }
}