using IdentityService.Models;
using IdentityService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
            .HasIndex(r => r.Name)
            .IsUnique();
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey("RoleId");
        
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = Guid.Parse("b1a6dcd6-6c30-4f69-91b2-f19f7d1f9c3a"), Name = "Admin" },
            new Role { Id = Guid.Parse("3f6c5d10-1e23-4f58-a7a3-c5f30c3b6a6d"), Name = "Manager" },
            new Role { Id = Guid.Parse("4d6d5e20-2a67-491e-91f3-d7f78c1c2e7f"), Name = "Worker" }
        );
        
        base.OnModelCreating(modelBuilder);
    }
}