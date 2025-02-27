using ClimateIndicesService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClimateIndicesService.Data;

public class ClimateIndicesDbContext(DbContextOptions<ClimateIndicesDbContext> options) : DbContext(options)
{
    public DbSet<DroughtRecord> DroughtRecords { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
    }
}