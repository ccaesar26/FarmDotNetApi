using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherService.Models.Entities;

public class WeatherCondition
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required]
    public int Code { get; init; }
    
    [Required, StringLength(50)]
    public string Description { get; init; } = string.Empty;
    
    [Required]
    public Guid AnimationId { get; init; } 
    
    [ForeignKey(nameof(AnimationId))]
    public WeatherAnimation? Animation { get; init; }
}