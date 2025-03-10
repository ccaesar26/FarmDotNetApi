using FarmerTasksService.Models.Enums;

namespace FarmerTasksService.Models.Dtos;

public record CreateTaskDto(
    string Title,
    string? Description,
    DateTime DueDate,
    TaskPriority Priority,
    Guid? AssignedUserId,
    Guid? CategoryId,
    RecurrenceType Recurrence, 
    DateTime? RecurrenceEndDate
);