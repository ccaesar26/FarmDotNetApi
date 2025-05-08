using Microsoft.EntityFrameworkCore;
using ReportsService.Models.Entities;

namespace ReportsService.Data;

public class ReportsDbContext(DbContextOptions<ReportsDbContext> options) : DbContext(options)
{
    // DbSet properties for each entity you want EF Core to manage
    public DbSet<Report> Reports { get; set; }
    public DbSet<ReportComment> ReportComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Call the base method first

        // --- Configure Report Entity ---
        modelBuilder.Entity<Report>(entity =>
        {
            // Define indexes for frequently queried columns
            entity.HasIndex(r => r.FarmId);
            entity.HasIndex(r => r.CreatedByUserId);
            entity.HasIndex(r => r.Status);
            entity.HasIndex(r => r.FieldId); // If you filter by FieldId often

            // Configure the one-to-many relationship with ReportComment
            // (Defined from the ReportComment side below, which is often clearer)
        });

        // --- Configure ReportComment Entity ---
        modelBuilder.Entity<ReportComment>(entity =>
        {
            // Define indexes
            entity.HasIndex(rc => rc.ReportId);
            entity.HasIndex(rc => rc.UserId);
            entity.HasIndex(rc => rc.ParentCommentId); // Important for querying threads efficiently

            // Configure the one-to-many relationship with Report
            entity.HasOne(rc => rc.Report)          // Each comment belongs to one Report
                  .WithMany(r => r.Comments)        // Each Report has many Comments
                  .HasForeignKey(rc => rc.ReportId) // The foreign key is ReportId
                  .OnDelete(DeleteBehavior.Cascade); // If a Report is deleted, delete all its comments

            // Configure the self-referencing one-to-many relationship for threading
            entity.HasOne(rc => rc.ParentComment)         // Each comment can have one parent comment
                  .WithMany(pc => pc.ChildComments)       // Each parent comment can have many child comments (replies)
                  .HasForeignKey(rc => rc.ParentCommentId) // The foreign key is ParentCommentId
                  .IsRequired(false)                      // ParentCommentId is nullable (top-level comments)
                  .OnDelete(DeleteBehavior.Restrict);     // Prevent deleting a comment if it has replies.
                                                          // Alternatively, use .SetNull if you want replies
                                                          // to become top-level if the parent is deleted.
                                                          // Cascade delete here would delete entire threads.
        });
    }
}