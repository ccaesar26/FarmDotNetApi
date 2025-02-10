using FarmProfileService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmProfileService.Data;

public class FarmProfileDbContext(DbContextOptions<FarmProfileDbContext> options) : DbContext(options)
{
    public DbSet<FarmProfile> FarmProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FarmProfile>()
            .HasIndex(f => f.Name);

        base.OnModelCreating(modelBuilder);
    }
}