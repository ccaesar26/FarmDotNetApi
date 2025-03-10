using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmerTasksService.Models.Entities;

public class TaskComment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public required Guid TaskId { get; set; }
    [ForeignKey(nameof(TaskId))]
    public TaskItem Task { get; set; } = null!;

    [Required]
    public required Guid UserId { get; set; } // ID of the user who created the comment

    [Required]
    public required DateTime CreatedAt { get; set; }

    [Required, MaxLength(500)] // Adjust max length as needed
    public required string Comment { get; set; }
}