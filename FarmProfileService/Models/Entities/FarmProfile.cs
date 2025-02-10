using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmProfileService.Models.Entities;

public class FarmProfile
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(120)]
    public required string Name { get; set; }
    
    [Required]
    [MaxLength(80)]
    public required string Country { get; set; }
    
    [Required]
    public Guid OwnerId { get; set; }
}