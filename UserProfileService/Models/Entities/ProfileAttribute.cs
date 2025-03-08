using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserProfileService.Models.Entities;

public class ProfileAttribute
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!; // "Harvester", "Planter", "Manager", custom names

    [Required]
    public Guid CategoryId { get; set; } // Foreign key to AttributeCategory

    [ForeignKey(nameof(CategoryId))]
    public AttributeCategory Category { get; set; } = null!;
    
    public bool IsPredefined { get; set; } = true;
    
    public ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>(); //For relationship
}