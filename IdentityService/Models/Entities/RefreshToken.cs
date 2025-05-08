using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.Models.Entities;

public class RefreshToken
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Token { get; set; }
    
    public DateTime ExpiryDate { get; set; }
    
    public bool IsRevoked { get; set; }
    
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    public virtual User User { get; set; }
}