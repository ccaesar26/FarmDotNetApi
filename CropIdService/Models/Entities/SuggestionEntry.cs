using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CropIdService.Models.Enums;

namespace CropIdService.Models.Entities;

public class SuggestionEntry
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    public required SuggestionType Type { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public double Probability { get; set; }
    
    [Required]
    public string ScientificName { get; set; } = string.Empty;
    
    // Foreign Key to IdEntry
    [Required]
    public Guid IdEntryId { get; set; }

    // Navigation Property to the parent IdEntry
    [ForeignKey(nameof(IdEntryId))]
    public IdEntry IdEntry { get; set; } = null!; // Initialize with null! for non-nullable reference types
}