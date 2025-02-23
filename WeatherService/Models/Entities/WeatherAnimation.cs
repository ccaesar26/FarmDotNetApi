using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherService.Models.Entities;

public class WeatherAnimation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required, StringLength(50)]
    public string Filename { get; init; } = string.Empty;
    
    [Required]
    public string LottieData { get; init; } = string.Empty;
    
    public string? FilenameDark { get; init; } = null;
    
    public string? LottieDataDark { get; init; } = null;
    
    public IList<WeatherCondition>? Conditions { get; init; } = [];
}