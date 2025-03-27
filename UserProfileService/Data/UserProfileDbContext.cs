using Microsoft.EntityFrameworkCore;
using UserProfileService.Models.Entities;

namespace UserProfileService.Data;

public class UserProfileDbContext(DbContextOptions<UserProfileDbContext> options) : DbContext(options)
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<ProfileAttribute> ProfileAttributes { get; set; }
    public DbSet<AttributeCategory> AttributeCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserProfile>()
            .HasIndex(u => u.UserId)
            .IsUnique();

        modelBuilder.Entity<UserProfile>()
            .HasMany(up => up.ProfileAttributes)
            .WithMany(pa => pa.UserProfiles)
            .UsingEntity(j => j.ToTable("UserProfileProfileAttribute"));
        
        modelBuilder.Entity<ProfileAttribute>()
            .HasOne(pa => pa.Category)
            .WithMany(ac => ac.ProfileAttributes)
            .HasForeignKey(pa => pa.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ProfileAttribute>()
            .HasIndex(pa => pa.Name)
            .IsUnique();
        
        // --- Data Seeding ---

        // 1. Attribute Categories
        var generalCategoryId = Guid.Parse("a1ef0f83-97a7-49c5-b0bd-2f6fbd6cd102");
        var cropSpecificCategoryId = Guid.Parse("e7aa259c-8b80-4a74-a99e-78d42f330a7c");
        var equipmentCategoryId = Guid.Parse("04c71678-4205-4a37-817b-92d05dc9fd72");
        var managerCategoryId = Guid.Parse("fee5d6bf-3a86-4800-a309-d0b0a4af5168"); // For the "Manager" attribute
        var customCategoryId = Guid.Parse("da69270b-6390-428f-92ed-b077f512d31f");

        modelBuilder.Entity<AttributeCategory>().HasData(
            new AttributeCategory { Id = generalCategoryId, Name = "General Farm Labor", IsPredefined = true },
            new AttributeCategory { Id = cropSpecificCategoryId, Name = "Crop-Specific Roles", IsPredefined = true },
            new AttributeCategory { Id = equipmentCategoryId, Name = "Equipment Specialization", IsPredefined = true },
            new AttributeCategory { Id = managerCategoryId, Name = "Administrative", IsPredefined = true }, // For "Manager"
            new AttributeCategory { Id = customCategoryId, Name = "Custom", IsPredefined = false } // For custom attributes
        );

        // 2. Profile Attributes
        modelBuilder.Entity<ProfileAttribute>().HasData(
            // General Farm Labor
            new ProfileAttribute { Id = Guid.Parse("59a1672f-0c87-4ec5-b52e-1ec09065c7ee"), Name = "General Farmhand/Laborer", CategoryId = generalCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("f5596fd6-ae03-4927-8a13-68e012a37d9d"), Name = "Field Worker", CategoryId = generalCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("2366a79e-5332-4b68-897e-e7de174fa69c"), Name = "Equipment Operator", CategoryId = generalCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("bd820d62-3e3c-4fad-9ad5-187fb055d9bf"), Name = "Maintenance Worker", CategoryId = generalCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("3c4b4a67-d9f4-4b10-bc52-8be1f5de2643"), Name = "Irrigation Technician", CategoryId = generalCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("19467a7f-2707-42be-8f7d-a179014936db"), Name = "Livestock Handler", CategoryId = generalCategoryId, IsPredefined = true },

            // Crop-Specific Roles
            new ProfileAttribute { Id = Guid.Parse("1c4e6180-9edb-4683-9afc-a0631dac381d"), Name = "Planter", CategoryId = cropSpecificCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("fab9ea3e-1589-45cb-98c8-9ed5c4148a38"), Name = "Harvester", CategoryId = cropSpecificCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("255c4fb4-76ed-4234-8ff5-9e7116224a40"), Name = "Weed Control Specialist", CategoryId = cropSpecificCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("a8a06283-6dd4-400d-8fd4-3800122d4856"), Name = "Pest Control Specialist", CategoryId = cropSpecificCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("093cefca-3ec1-4f52-a39c-62dedbc82a61"), Name = "Scout", CategoryId = cropSpecificCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("911c9119-6570-4070-9a0d-9a5301b9773b"), Name = "Crop Specialist / Agronomist Assistant", CategoryId = cropSpecificCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("5d85c8f3-774e-47f7-8b4b-5334f1a3722f"), Name = "Greenhouse Worker", CategoryId = cropSpecificCategoryId, IsPredefined = true },

            // Equipment Specialization
            new ProfileAttribute { Id = Guid.Parse("bb96978e-f161-4b91-98d8-eb7098727d77"), Name = "Tractor Operator", CategoryId = equipmentCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("08312aa3-68b0-42a5-9522-aeebb5b4d85d"), Name = "Combine Operator", CategoryId = equipmentCategoryId, IsPredefined = true },
            new ProfileAttribute { Id = Guid.Parse("b00c6b53-65cd-43d5-a9a6-83149923a0ee"), Name = "Sprayer Operator", CategoryId = equipmentCategoryId, IsPredefined = true },

            // Manager Attribute (Crucial)
            new ProfileAttribute { Id = Guid.Parse("3fa29a42-7763-4c0d-b391-458de8e79a33"), Name = "Manager", CategoryId = managerCategoryId, IsPredefined = true }
        );
    }
}