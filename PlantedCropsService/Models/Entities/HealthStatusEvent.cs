using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlantedCropsService.Models.Enums;

namespace PlantedCropsService.Models.Entities;

public class HealthStatusEvent
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required] 
    public required Guid CropId { get; set; }
    public virtual Crop Crop { get; set; } = null!;

    [Required] 
    public required string HealthStatus { get; set; }

    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow; // Default to current UTC time

    [Required] 
    public required Guid RecordedByUserId { get; set; }

    [Required] 
    public required string RecordedByUserName { get; set; }

    public string? PestOrDisease { get; set; }

    [Required]
    public required SeverityLevel SeverityLevel { get; set; } // Using the enum

    public string? TreatmentApplied { get; set; }

    // Many-to-many relationship with Note
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}