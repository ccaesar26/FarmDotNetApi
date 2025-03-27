using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IdentityService.Models.Entities;

namespace IdentityService.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }
    
    [Required]
    [MaxLength(120)]
    public required string Username { get; set; }

    [Required]
    [MaxLength(120)]
    [EmailAddress]
    public required string Email { get; set; }
    
    [Required]
    [MaxLength(120)]
    public required string PasswordHash { get; init; }
    
    public required Role Role { get; set; }
    
    public Guid? FarmId { get; set; }
    
    public Guid? UserProfileId { get; set; }
}