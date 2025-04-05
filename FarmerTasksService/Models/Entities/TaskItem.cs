using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FarmerTasksService.Models.Enums;
using TaskStatus = FarmerTasksService.Models.Enums.TaskStatus;

namespace FarmerTasksService.Models.Entities;

public class TaskItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required, MaxLength(200)]
    public required string Title { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    [Required]
    public required TaskPriority Priority { get; set; }

    [Required]
    public required TaskStatus Status { get; set; }

    public Guid? CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public TaskCategory? Category { get; set; }
    
    public RecurrenceType Recurrence { get; set; } = RecurrenceType.None;
    public DateTime? RecurrenceEndDate { get; set; }
    public DateTime? LastGeneratedDate { get; set; }
    
    public virtual ICollection<TaskComment> Comments { get; init; } = new List<TaskComment>();
    
    public virtual ICollection<TaskAssignment> TaskAssignments { get; init; } = new List<TaskAssignment>();
    
    public Guid? FieldId { get; set; } // Foreign key to Field (in another service)
    
    public Guid? CropId { get; set; }

    // Farm/Owner ID (add this)
    [Required]
    public Guid FarmId { get; set; }  // Assuming you'll use FarmId (more common)
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}