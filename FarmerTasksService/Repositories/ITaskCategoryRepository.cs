using FarmerTasksService.Models.Entities;

namespace FarmerTasksService.Repositories;

public interface ITaskCategoryRepository
{
    ValueTask<List<TaskCategory>> GetAllAsync();
    ValueTask<TaskCategory?> GetByIdAsync(Guid id);
}