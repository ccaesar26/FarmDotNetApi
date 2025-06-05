using CropIdService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CropIdService.Data;

public class CropIdDbContext(DbContextOptions<CropIdDbContext> options) : DbContext(options)
{
    public DbSet<IdEntry> IdEntries { get; set; }
    public DbSet<SuggestionEntry> SuggestionEntries { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Call base method first

        modelBuilder.Entity<IdEntry>(entity =>
        {
            // Configure the one-to-many relationship from the IdEntry side
            entity.HasMany(ie => ie.Suggestions)         // IdEntry has many Suggestions
                .WithOne(se => se.IdEntry)             // Each SuggestionEntry has one IdEntry
                .HasForeignKey(se => se.IdEntryId)     // The foreign key in SuggestionEntry is IdEntryId
                .OnDelete(DeleteBehavior.Cascade);     // If an IdEntry is deleted, delete all its suggestions.
            // Choose DeleteBehavior.Restrict if you don't want to delete
            // suggestions automatically and want to handle it manually or
            // prevent deletion if suggestions exist.
            // DeleteBehavior.SetNull is also an option if IdEntryId is nullable
            // (but it's required in your case).
        });

        // You don't strictly need to configure the relationship from the SuggestionEntry side
        // if you've done it from the IdEntry side, as EF Core can infer the reverse.
        // However, if you want to be explicit or configure something specific from that side:
        // modelBuilder.Entity<SuggestionEntry>(entity =>
        // {
        //     entity.HasOne(se => se.IdEntry)
        //           .WithMany(ie => ie.Suggestions)
        //           .HasForeignKey(se => se.IdEntryId);
        // });
    }
}