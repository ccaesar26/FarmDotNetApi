using Microsoft.EntityFrameworkCore;
using PlantedCropsService.Models.Entities;

namespace PlantedCropsService.Data;

public class CropsDbContext(DbContextOptions<CropsDbContext> options) : DbContext(options)
{
    public DbSet<Crop> Crops { get; set; }
    public DbSet<GrowthStageEvent> GrowthStageEvents { get; set; }
    public DbSet<HealthStatusEvent> HealthStatusEvents { get; set; }
    public DbSet<FertilizerEvent> FertilizerEvents { get; set; }
    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Many-to-Many relationships using Fluent API (optional but good practice)

        // GrowthStageEvent <-> Note
        modelBuilder.Entity<GrowthStageEvent>()
            .HasMany(gse => gse.Notes)
            .WithMany(n => n.GrowthStageEvents)
            .UsingEntity(j => j.ToTable("GrowthStageEventNote")); // Optional: customize the junction table name

        // HealthStatusEvent <-> Note
        modelBuilder.Entity<HealthStatusEvent>()
            .HasMany(hse => hse.Notes)
            .WithMany(n => n.HealthStatusEvents)
            .UsingEntity(j => j.ToTable("HealthStatusEventNote")); // Optional: customize the junction table name

        // FertilizerEvent <-> Note
        modelBuilder.Entity<FertilizerEvent>()
            .HasMany(fe => fe.Notes)
            .WithMany(n => n.FertilizerEvents)
            .UsingEntity(j => j.ToTable("FertilizerEventNote")); // Optional: customize the junction table name


        // Configure Enum for SeverityLevel (optional, but good practice for clarity)
        modelBuilder.Entity<HealthStatusEvent>()
            .Property(e => e.SeverityLevel)
            .HasConversion<string>(); // Stores the enum as a string in the database (alternatives: int, etc.)
    }
}