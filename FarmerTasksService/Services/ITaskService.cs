using FarmerTasksService.Models.Dtos;
using FarmerTasksService.Models.Entities;
using TaskStatus = FarmerTasksService.Models.Enums.TaskStatus;

namespace FarmerTasksService.Services;

public interface ITaskService
{
    ValueTask<Guid> CreateTaskAsync(CreateTaskDto dto, Guid farmId);
    ValueTask<TaskDto?> GetTaskByIdAsync(Guid id);
    ValueTask<List<TaskDto>> GetTasksAsync(TaskFilterDto filter, int pageNumber, int pageSize, Guid farmId);
    ValueTask UpdateTaskAsync(Guid id, UpdateTaskDto dto);
    ValueTask DeleteTaskAsync(Guid id);
    ValueTask AssignUsersToTaskAsync(Guid taskId, List<Guid> userIds);
    ValueTask UnassignUsersFromTaskAsync(Guid taskId, List<Guid> userIds);
    ValueTask UpdateTaskStatusAsync(Guid taskId, TaskStatus status);
    ValueTask<List<TaskDto>> GetMyTasksAsync(Guid userId, int pageNumber, int pageSize);
    ValueTask<List<TaskCategory>> GetTaskCategoriesAsync();
    ValueTask GenerateRecurringTasksAsync();
    ValueTask<Guid> AddCommentAsync(CreateTaskCommentDto dto);
    ValueTask<List<TaskCommentDto>> GetCommentsByTaskIdAsync(Guid taskId);
}