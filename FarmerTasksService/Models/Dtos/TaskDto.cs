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
    List<Guid> AssignedUserIds,
    Guid? CategoryId,
    string? CategoryName,
    RecurrenceType Recurrence,
    DateTime? RecurrenceEndDate,
    Guid? FieldId,
    Guid? CropId,
    int CommentsCount,
    DateTime CreatedAt
);