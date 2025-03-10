using FarmerTasksService.Extensions;
using FarmerTasksService.Models.Dtos;
using FarmerTasksService.Models.Entities;
using FarmerTasksService.Repositories;
using TaskStatus = FarmerTasksService.Models.Enums.TaskStatus;

namespace FarmerTasksService.Services;

public class TaskService(
    ITaskRepository taskRepository,
    ITaskCategoryRepository taskCategoryRepository,
    ITaskCommentRepository taskCommentRepository,
    ILogger<TaskService> logger
) : ITaskService
{
    public async ValueTask<Guid> CreateTaskAsync(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Priority = dto.Priority,
            Status = TaskStatus.ToDo,
            AssignedUserId = dto.AssignedUserId,
            CategoryId = dto.CategoryId,
            Recurrence = dto.Recurrence,
            RecurrenceEndDate = dto.RecurrenceEndDate,
        };

        await taskRepository.AddAsync(task);
        return task.Id;
    }

    public async ValueTask<TaskDto?> GetTaskByIdAsync(Guid id)
    {
        var task = await taskRepository.GetByIdAsync(id);
        return task?.ToDto();
    }

    public async ValueTask<List<TaskDto>> GetTasksAsync(TaskFilterDto filter)
    {
        var tasks = await taskRepository.GetAllAsync(filter);
        return tasks.Select(t => t.ToDto()).ToList();
    }

    public async ValueTask UpdateTaskAsync(Guid id, UpdateTaskDto dto)
    {
        var task = await taskRepository.GetByIdAsync(id);
        if (task == null)
        {
            throw new Exception($"Task with id {id} not found");
        }

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.DueDate = dto.DueDate;
        task.Priority = dto.Priority;
        task.Status = dto.Status;
        task.AssignedUserId = dto.AssignedUserId;
        task.CategoryId = dto.CategoryId;
        task.Recurrence = dto.Recurrence;
        task.RecurrenceEndDate = dto.RecurrenceEndDate;

        await taskRepository.UpdateAsync(task);
    }

    public async ValueTask DeleteTaskAsync(Guid id) => await taskRepository.DeleteAsync(id);

    public async ValueTask AssignTaskAsync(Guid taskId, Guid userId)
    {
        var task = await taskRepository.GetByIdAsync(taskId);
        if (task == null)
        {
            throw new Exception($"Task with id {taskId} not found");
        }
        
        task.AssignedUserId = userId;
        await taskRepository.UpdateAsync(task);
    }

    public async ValueTask UnassignTaskAsync(Guid taskId)
    {
        var task = await taskRepository.GetByIdAsync(taskId);
        if (task == null)
        {
            throw new Exception($"Task with id {taskId} not found");
        }
        
        task.AssignedUserId = null;
        await taskRepository.UpdateAsync(task);
    }

    public async ValueTask UpdateTaskStatusAsync(Guid taskId, TaskStatus status)
    {
        var task = await taskRepository.GetByIdAsync(taskId);
        if (task == null)
        {
            throw new Exception($"Task with id {taskId} not found");
        }
        
        task.Status = status;
        await taskRepository.UpdateAsync(task);
    }

    public async ValueTask<List<TaskDto>> GetMyTasksAsync(Guid userId)
    {
        var tasks = await taskRepository.GetByUserIdAsync(userId);
        return tasks.Select(t => t.ToDto()).ToList();
    }

    public async ValueTask<List<TaskCategory>> GetTaskCategoriesAsync() => 
        await taskCategoryRepository.GetAllAsync();

    public async ValueTask<Guid> AddCommentAsync(CreateTaskCommentDto dto)
    {
        var comment = dto.ToEntity();

        await taskCommentRepository.AddAsync(comment);
        return comment.Id;
    }

    public async ValueTask<List<TaskCommentDto>> GetCommentsByTaskIdAsync(Guid taskId)
    {
        var comments = await taskCommentRepository.GetByTaskIdAsync(taskId);
        return comments.Select(c => c.ToDto()).ToList();
    }
}