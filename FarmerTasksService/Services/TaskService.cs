using FarmerTasksService.Extensions;
using FarmerTasksService.Models.Dtos;
using FarmerTasksService.Models.Entities;
using FarmerTasksService.Models.Enums;
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
    public async ValueTask<Guid> CreateTaskAsync(CreateTaskDto dto, Guid farmId)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            Priority = dto.Priority,
            Status = dto.Status,
            CategoryId = dto.CategoryId,
            Recurrence = dto.Recurrence,
            RecurrenceEndDate = dto.RecurrenceEndDate,
            FieldId = dto.FieldId,
            FarmId = farmId
        };

        await taskRepository.AddAsync(task);
        return task.Id;
    }

    public async ValueTask<TaskDto?> GetTaskByIdAsync(Guid id) => (await taskRepository.GetByIdAsync(id))?.ToDto();

    public async ValueTask<List<TaskDto>> GetTasksAsync(TaskFilterDto filter, int pageNumber, int pageSize, Guid farmId)
        => (await taskRepository.GetAllAsync(farmId, filter, pageNumber, pageSize))
            .Select(t => t.ToDto())
            .ToList();

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
        task.CategoryId = dto.CategoryId;
        task.Recurrence = dto.Recurrence;
        task.RecurrenceEndDate = dto.RecurrenceEndDate;
        task.FieldId = dto.FieldId;

        await taskRepository.UpdateAsync(task);
    }

    public async ValueTask DeleteTaskAsync(Guid id) => await taskRepository.DeleteAsync(id);

    public async ValueTask AssignUsersToTaskAsync(Guid taskId, List<Guid> userIds) =>
        await taskRepository.AssignUsersToTaskAsync(taskId, userIds);

    public async ValueTask UnassignUsersFromTaskAsync(Guid taskId, List<Guid> userIds) =>
        await taskRepository.UnassignUsersFromTaskAsync(taskId, userIds);

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

    public async ValueTask<List<TaskDto>> GetMyTasksAsync(Guid userId, int pageNumber, int pageSize) =>
        (await taskRepository.GetByAssignedUserAsync(userId, pageNumber, pageSize))
        .Select(t => t.ToDto())
        .ToList();

    public async ValueTask<List<TaskCategory>> GetTaskCategoriesAsync() => await taskCategoryRepository.GetAllAsync();

    public async ValueTask GenerateRecurringTasksAsync()
    {
        var recurringTasks = await taskRepository.GetAllNotGeneratedRecurringAsync();
        
        foreach (var task in recurringTasks)
        {
            var nextDueDate = task.DueDate switch
            {
                null => throw new Exception("Task due date is required for recurring tasks"),
                _ => task.Recurrence switch
                {
                    RecurrenceType.None => throw new Exception("Task recurrence type is required for recurring tasks"),
                    RecurrenceType.Daily => task.DueDate.Value.AddDays(1),
                    RecurrenceType.Weekly => task.DueDate.Value.AddDays(7),
                    RecurrenceType.Monthly => task.DueDate.Value.AddMonths(1),
                    RecurrenceType.Yearly => task.DueDate.Value.AddYears(1),
                    _ => throw new Exception("Invalid recurrence type")
                }
            };
            
            if (task.RecurrenceEndDate.HasValue && nextDueDate > task.RecurrenceEndDate.Value)
            {
                continue; // Skip if past the end date
            }
            
            var newTask = new TaskItem
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = nextDueDate,
                Priority = task.Priority,
                Status = TaskStatus.ToDo,
                CategoryId = task.CategoryId,
                Recurrence = task.Recurrence,
                RecurrenceEndDate = task.RecurrenceEndDate,
                FieldId = task.FieldId,
                FarmId = task.FarmId,
                LastGeneratedDate = task.LastGeneratedDate
            };

            await taskRepository.AddAsync(newTask);

            // Update last generated date
            task.LastGeneratedDate = DateTime.UtcNow;
            await taskRepository.UpdateAsync(task);
        }
    }

    public async ValueTask<Guid> AddCommentAsync(CreateTaskCommentDto dto) =>
        (await taskCommentRepository.AddAsync(dto.ToEntity())).Id;


    public async ValueTask<List<TaskCommentDto>> GetCommentsByTaskIdAsync(Guid taskId) =>
        (await taskCommentRepository.GetByTaskIdAsync(taskId)).Select(c => c.ToDto()).ToList();
}