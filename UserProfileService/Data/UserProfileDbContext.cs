using Microsoft.EntityFrameworkCore;
using UserProfileService.Models.Entities;

namespace UserProfileService.Data;

public class UserProfileDbContext(DbContextOptions<UserProfileDbContext> options) : DbContext(options)
{
    public DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfile>()
            .HasIndex(u => u.UserId)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
    
}