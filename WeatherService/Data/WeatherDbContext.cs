using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WeatherService.Models.Dtos;
using WeatherService.Models.Entities;

namespace WeatherService.Data;

public class WeatherDbContext(DbContextOptions<WeatherDbContext> options) : DbContext(options)
{
    public DbSet<WeatherAnimation> WeatherAnimations { get; set; }
    public DbSet<WeatherCondition> WeatherConditions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Construct the base path to your project's directory
        var basePath = Directory.GetCurrentDirectory();
        var assetsFolderPath = Path.Combine(basePath, "Assets");
        // If you are running the command from the solution root, you might need to adjust the path
        // to point to your WeatherService project directory.
        // Example if you are in the solution root:
        // basePath = Path.Combine(basePath, "WeatherService"); // Adjust "WeatherService" to your project name if needed
    
        var weatherAnimationsJsonPath = Path.Combine(assetsFolderPath, "WeatherAnimations.json");
        var weatherAnimationsJson = File.ReadAllText(weatherAnimationsJsonPath);
        var weatherAnimationDtos = JsonSerializer.Deserialize<List<WeatherAnimationDto>>(weatherAnimationsJson) ?? [];
    
        var weatherAnimations = new List<WeatherAnimation>();
        foreach (var (id, filename, filenameDark, _) in weatherAnimationDtos)
        {
            var lottieDataJsonPath = Path.Combine(assetsFolderPath, "Animations", filename);
            var lottieDataJson = File.ReadAllText(lottieDataJsonPath);

            string? lottieDataDarkJson = null;
            if (!string.IsNullOrEmpty(filenameDark))
            {
                var lottieDataDarkJsonPath = Path.Combine(assetsFolderPath, "Animations", filenameDark);
                lottieDataDarkJson = File.ReadAllText(lottieDataDarkJsonPath);
            }

            weatherAnimations.Add(new WeatherAnimation
            {
                Id = id,
                Filename = filename,
                LottieData = lottieDataJson,
                FilenameDark = filenameDark,
                LottieDataDark = lottieDataDarkJson
            });
        }
    
        var weatherConditionsJsonPath = Path.Combine(assetsFolderPath, "WeatherConditions.json");
        var weatherConditionsJson = File.ReadAllText(weatherConditionsJsonPath);
        var weatherConditionDtos = JsonSerializer.Deserialize<List<WeatherConditionDto>>(weatherConditionsJson) ?? [];
    
        var weatherConditions = new List<WeatherCondition>();
        foreach (var weatherConditionDto in weatherConditionDtos)
        {
            var animationDto = weatherAnimationDtos.FirstOrDefault(wa => wa.codes.Contains(weatherConditionDto.code));
            if (animationDto != null)
            {
                weatherConditions.Add(new WeatherCondition
                {
                    Id = weatherConditionDto.id,
                    Code = weatherConditionDto.code,
                    Description = weatherConditionDto.description,
                    AnimationId = animationDto.id
                });
            }
            else
            {
                // Handle cases where no animation is found for a condition code (optional logging)
                Console.WriteLine($"Warning: No animation found for weather code: {weatherConditionDto.code}");
            }
        }
        
        modelBuilder.Entity<WeatherAnimation>().HasData(weatherAnimations);
        modelBuilder.Entity<WeatherCondition>().HasData(weatherConditions);
    
        base.OnModelCreating(modelBuilder);
    }
}