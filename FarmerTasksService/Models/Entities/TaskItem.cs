using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FarmerTasksService.Models.Enums;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace FarmerTasksService.Models.Entities;

public class TaskItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required, MaxLength(200)]
    public required string Title { get; set; }

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    [Required]
    public required TaskPriority Priority { get; set; }

    [Required]
    public required TaskStatus Status { get; set; }

    public Guid? AssignedUserId { get; set; }

    public Guid? CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public TaskCategory? Category { get; set; }
}