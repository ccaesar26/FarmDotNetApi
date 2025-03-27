using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using CropCatalogService.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace CropCatalogService.Data;

public class CropCatalogDbContext(DbContextOptions<CropCatalogDbContext> options) : DbContext(options)
{
    public DbSet<CropCatalogEntry> CropCatalogEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var resourceName = "CropCatalogService.SeedData.crop-catalog-seed.json";

        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName)!;
            using var reader = new StreamReader(stream!);
            var seedDataJson = reader.ReadToEnd();

            var seedData = JsonSerializer.Deserialize<CropCatalogEntry[]>(seedDataJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new DateOnlyConverter() } // Register the converter
            });

            if (seedData != null)
            {
                modelBuilder.Entity<CropCatalogEntry>().HasData(seedData);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading embedded seed data resource: {ex.Message}");
            // Log the error properly in a real application
        }
    }
}

public class DateOnlyConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}