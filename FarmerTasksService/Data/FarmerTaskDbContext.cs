using FarmerTasksService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmerTasksService.Data;

public class FarmerTaskDbContext(DbContextOptions<FarmerTaskDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<TaskCategory> TaskCategories { get; set; } // If using categories
    public DbSet<TaskComment> TaskComments {get; set;}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.SetNull); // Or another appropriate action

        modelBuilder.Entity<TaskComment>()
            .HasOne(c => c.Task)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskId)
            .OnDelete(DeleteBehavior.Cascade); // Delete comments when a task is deleted.
        
        var plantingCategoryId = Guid.Parse("ba84a42e-b766-4778-bbae-94429ffcd66c");
        var harvestingCategoryId = Guid.Parse("e41ec2cb-6567-434c-b079-7d5d13e8d194");
        var maintenanceCategoryId = Guid.Parse("6d57eb9f-c3d0-4fbe-8f35-a7fb5c906b91");
        var irrigationCategoryId = Guid.Parse("d332a3b8-af4d-460e-8c35-a6acfd2d71c8");
        var pestControlCategoryId = Guid.Parse("b01abce0-1604-483b-8c7d-ffc8de5c459e");
        
        modelBuilder.Entity<TaskCategory>().HasData(
            new TaskCategory { Id = plantingCategoryId, Name = "Planting" },
            new TaskCategory { Id = harvestingCategoryId, Name = "Harvesting" },
            new TaskCategory { Id = maintenanceCategoryId, Name = "Maintenance" },
            new TaskCategory { Id = irrigationCategoryId, Name = "Irrigation" },
            new TaskCategory { Id = pestControlCategoryId, Name = "Pest and Disease Control" }
        );
    }
}