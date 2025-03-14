using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmerTasksService.Models.Entities;

public class TaskCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }

    [Required, MaxLength(100)]
    public required string Name { get; init; } // e.g., "Planting", "Harvesting", "Maintenance"

    public ICollection<TaskItem> Tasks { get; init; } = new List<TaskItem>();
}