using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantedCropsService.Models.Entities;

public class GrowthStageEvent
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required] 
    public required Guid CropId { get; set; }
    public virtual Crop Crop { get; set; } = null!;

    [Required] 
    public required string Stage { get; set; }

    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow; 

    [Required] 
    public required Guid RecordedByUserId { get; set; }

    [Required] 
    public required string RecordedByUserName { get; set; }

    // Many-to-many relationship with Note
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}