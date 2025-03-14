using FarmerTasksService.Models.Enums;
using TaskStatus = FarmerTasksService.Models.Enums.TaskStatus;

namespace FarmerTasksService.Models.Dtos;

public record CreateTaskDto(
    string Title,
    string? Description,
    DateTime? DueDate,
    TaskPriority Priority,
    Guid? CategoryId,
    RecurrenceType Recurrence,
    DateTime? RecurrenceEndDate,
    Guid FieldId,
    TaskStatus Status = TaskStatus.ToDo
);