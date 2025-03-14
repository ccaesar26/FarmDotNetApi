using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmerTasksService.Models.Entities;

public class TaskAssignment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }
    
    [Required]
    public Guid TaskId { get; init; }
    [ForeignKey(nameof(TaskId))]
    public TaskItem Task { get; init; } = null!;
    
    [Required]
    public Guid UserId { get; init; }
    
    public DateTime AssignedAt { get; init; } = DateTime.UtcNow;
}