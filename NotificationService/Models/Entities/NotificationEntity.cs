using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Models.Entities;

public class NotificationEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string NotificationType { get; set; }
    
    [Required]
    public required Guid SourceEntityId { get; set; }
    
    [Required]
    public required Guid TriggeringUserId { get; set; }
    
    [Required]
    public required DateTime Timestamp { get; set; }

    [Required]
    public required Guid TargetFarmId { get; set; }
    
    public Guid? TargetUserId { get; set; } 
    
    [Required]
    public required bool IsRead { get; set; }
}