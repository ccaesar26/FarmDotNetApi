using FarmerTasksService.Models.Enums;
using TaskStatus = FarmerTasksService.Models.Enums.TaskStatus;

namespace FarmerTasksService.Models.Dtos;

public record TaskDto(
    Guid Id,
    string Title,
    string? Description,
    DateTime? DueDate,
    TaskPriority Priority,
    TaskStatus Status,
    Guid? AssignedUserId,
    Guid? CategoryId,
    string? CategoryName,
    RecurrenceType Recurrence,
    DateTime? RecurrenceEndDate
);