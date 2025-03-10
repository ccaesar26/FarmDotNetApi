using FarmerTasksService.Models.Enums;
using TaskStatus = FarmerTasksService.Models.Enums.TaskStatus;

namespace FarmerTasksService.Models.Dtos;

public record TaskFilterDto(
    TaskStatus? Status,
    TaskPriority? Priority,
    Guid? CategoryId,
    Guid? AssignedUserId,
    DateTime? DueDateStart,
    DateTime? DueDateEnd
);