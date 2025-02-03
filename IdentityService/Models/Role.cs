using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.Models;

public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }

    [Required]
    [MaxLength(50)]
    public required string Name { get; init; }
    
    public ICollection<User> Users { get; set; } = new List<User>();
}