using FarmerTasksService.Models.Enums;
using TaskStatus = FarmerTasksService.Models.Enums.TaskStatus;

namespace FarmerTasksService.Models.Dtos;

public record UpdateTaskDto(
    string Title,
    string? Description,
    DateTime DueDate,
    TaskPriority Priority,
    TaskStatus Status,
    Guid? AssignedUserId,
    Guid? CategoryId,
    RecurrenceType Recurrence,
    DateTime? RecurrenceEndDate
);