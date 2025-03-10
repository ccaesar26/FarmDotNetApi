using FarmerTasksService.Models.Dtos;
using FarmerTasksService.Models.Entities;

namespace FarmerTasksService.Repositories;

public interface ITaskRepository
{
    ValueTask<TaskItem> AddAsync(TaskItem task);
    ValueTask<TaskItem?> GetByIdAsync(Guid id);
    ValueTask<List<TaskItem>> GetAllAsync(TaskFilterDto? filter = null); // Include filtering
    ValueTask UpdateAsync(TaskItem task);
    ValueTask DeleteAsync(Guid id);
    ValueTask<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId);
}