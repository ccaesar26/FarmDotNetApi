using FarmerTasksService.Models.Dtos;
using FarmerTasksService.Models.Entities;

namespace FarmerTasksService.Extensions;

public static class TaskExtensions
{
    public static TaskDto ToDto(this TaskItem task) => new TaskDto(
        task.Id,
        task.Title,
        task.Description,
        task.DueDate,
        task.Priority,
        task.Status,
        task.AssignedUserId,
        task.CategoryId,
        task.Category?.Name,
        task.Recurrence,
        task.RecurrenceEndDate
    );
}