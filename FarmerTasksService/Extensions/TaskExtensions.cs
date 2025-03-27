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
        task.TaskAssignments.Select(x => x.UserId).ToList(),
        task.CategoryId,
        task.Category?.Name,
        task.Recurrence,
        task.RecurrenceEndDate,
        task.FieldId,
        task.Comments.Count,
        task.CreatedAt
    );
    
    public static TaskCategoryDto ToDto(this TaskCategory category) => new TaskCategoryDto(
        category.Id,
        category.Name
    );
}