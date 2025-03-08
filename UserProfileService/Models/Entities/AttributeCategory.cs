using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserProfileService.Models.Entities;

public class AttributeCategory // New Entity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required, MaxLength(50)]
    public required string Name { get; set; } // "Skills", "Certifications", "Custom", etc.

    public ICollection<ProfileAttribute> ProfileAttributes { get; set; } = new List<ProfileAttribute>(); // Relation to ProfileAttribute

    public bool IsPredefined { get; set; } = true;
}