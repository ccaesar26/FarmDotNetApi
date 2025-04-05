using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlantedCropsService.Models.Entities;

public class FertilizerEvent
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required] 
    public required Guid CropId { get; set; }
    public virtual Crop Crop { get; set; } = null!;

    [Required] 
    public required string FertilizerType { get; set; }

    [Required, Column(TypeName = "date")] // Specify date type in database
    public required DateOnly ApplicationDate { get; set; }

    [Required]
    public required double QuantityApplied { get; set; }

    [Required]
    public required string Units { get; set; }
    
    public string? ApplicationMethod { get; set; }

    public string? EquipmentUsed { get; set; }

    [Required] 
    public required Guid AppliedByUserId { get; set; }

    [Required] 
    public required string AppliedByUserName { get; set; }

    // Many-to-many relationship with Note
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}