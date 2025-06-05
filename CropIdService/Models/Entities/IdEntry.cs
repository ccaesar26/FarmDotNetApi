using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CropIdService.Models.Entities;

public class IdEntry
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public double Longitude { get; set; }
    
    [Required]
    public double Latitude { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FieldName { get; set; } = string.Empty;
    
    [Required]
    public Guid FarmId { get; set; } 
    
    [Required]
    public string ImageBase64Data { get; set; } = string.Empty;
    
    [Required]
    public DateTime Datetime { get; set; }
    
    [Required]
    public bool IsPlant { get; set; }
    
    // Collection Navigation Property for Suggestions
    public ICollection<SuggestionEntry> Suggestions { get; set; } = new List<SuggestionEntry>();
}