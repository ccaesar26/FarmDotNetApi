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
    public required string PasswordHash { get; init; }
    
    public required Role Role { get; init; }
    
    public Guid? FarmId { get; set; }
}