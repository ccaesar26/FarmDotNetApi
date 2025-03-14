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

    // public Guid? AssignedUserId { get; set; }

    public Guid? CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public TaskCategory? Category { get; set; }
    
    public RecurrenceType Recurrence { get; set; } = RecurrenceType.None;
    public DateTime? RecurrenceEndDate { get; set; }
    public DateTime? LastGeneratedDate { get; set; }
    
    public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
    
    public ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();
    
    // Field Relationship (One-to-Many for now)
    [Required] // Assuming a task MUST be linked to a Field
    public Guid FieldId { get; set; } // Foreign key to Field (in another service)

    // Farm/Owner ID (add this)
    [Required]
    public Guid FarmId { get; set; }  // Assuming you'll use FarmId (more common)
}