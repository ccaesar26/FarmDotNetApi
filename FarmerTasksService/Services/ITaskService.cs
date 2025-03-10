using FarmerTasksService.Models.Dtos;
using FarmerTasksService.Models.Entities;
using TaskStatus = FarmerTasksService.Models.Enums.TaskStatus;

namespace FarmerTasksService.Services;

public interface ITaskService
{
    ValueTask<Guid> CreateTaskAsync(CreateTaskDto dto);
    ValueTask<TaskDto?> GetTaskByIdAsync(Guid id);
    ValueTask<List<TaskDto>> GetTasksAsync(TaskFilterDto filter);
    ValueTask UpdateTaskAsync(Guid id, UpdateTaskDto dto);
    ValueTask DeleteTaskAsync(Guid id);
    ValueTask AssignTaskAsync(Guid taskId, Guid userId);
    ValueTask UnassignTaskAsync(Guid taskId);
    ValueTask UpdateTaskStatusAsync(Guid taskId, TaskStatus status);
    ValueTask<List<TaskDto>> GetMyTasksAsync(Guid userId);
    ValueTask<List<TaskCategory>> GetTaskCategoriesAsync(); //if categories
    ValueTask<Guid> AddCommentAsync(CreateTaskCommentDto dto);
    ValueTask<List<TaskCommentDto>> GetCommentsByTaskIdAsync(Guid taskId);
}