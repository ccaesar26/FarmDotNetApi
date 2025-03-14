using FarmerTasksService.Models.Dtos;
using FarmerTasksService.Models.Entities;

namespace FarmerTasksService.Repositories;

public interface ITaskRepository
{
    ValueTask<TaskItem> AddAsync(TaskItem task);
    ValueTask<TaskItem?> GetByIdAsync(Guid id);
    ValueTask<List<TaskItem>> GetAllAsync(Guid farmId, TaskFilterDto? filter = null, int pageNumber = 1,
        int pageSize = int.MaxValue);
    ValueTask UpdateAsync(TaskItem task);
    ValueTask DeleteAsync(Guid id);
    ValueTask<IEnumerable<TaskItem>> GetByAssignedUserAsync(Guid userId, int pageNumber, int pageSize);
    ValueTask AssignUsersToTaskAsync(Guid taskId, List<Guid> userIds);
    ValueTask UnassignUsersFromTaskAsync(Guid taskId, List<Guid> userIds);
    ValueTask<IEnumerable<TaskItem>> GetAllNotGeneratedRecurringAsync();
}