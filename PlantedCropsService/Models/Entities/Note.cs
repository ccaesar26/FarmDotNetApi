using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantedCropsService.Models.Entities;

public class Note
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required] 
    public required string Text { get; set; }

    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Default to current UTC time

    [Required] 
    public required Guid AuthorUserId { get; set; }

    [Required] 
    public required string AuthorUserName { get; set; }

    // Navigation properties for many-to-many relationships
    public virtual ICollection<GrowthStageEvent> GrowthStageEvents { get; set; } = new List<GrowthStageEvent>();
    public virtual ICollection<HealthStatusEvent> HealthStatusEvents { get; set; } = new List<HealthStatusEvent>();
    public virtual ICollection<FertilizerEvent> FertilizerEvents { get; set; } = new List<FertilizerEvent>();
}