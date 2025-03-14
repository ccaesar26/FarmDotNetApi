using FarmerTasksService.Models.Enums;
using TaskStatus = FarmerTasksService.Models.Enums.TaskStatus;

namespace FarmerTasksService.Models.Dtos;

public record TaskFilterDto(
    TaskStatus? Status,
    TaskPriority? Priority,
    Guid? CategoryId,
    DateTime? DueDateStart,
    DateTime? DueDateEnd
);