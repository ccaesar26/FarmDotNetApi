using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }
    
    [Required]
    [MaxLength(120)]
    public required string Username { get; init; }

    [Required]
    [MaxLength(120)]
    [EmailAddress]
    public required string Email { get; init; }
    
    [Required]
    [MaxLength(120)]
    public required string PasswordHash { get; set; }
    
    public Guid? FarmId { get; init; }
    
    public ICollection<Role> Roles { get; init; } = new List<Role>();
}