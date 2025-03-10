using FarmerTasksService.Data;
using FarmerTasksService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmerTasksService.Repositories;

public class TaskCategoryRepository(FarmerTaskDbContext context) : ITaskCategoryRepository
{
    public async ValueTask<List<TaskCategory>> GetAllAsync() => 
        await context.TaskCategories.ToListAsync();

    public async ValueTask<TaskCategory?> GetByIdAsync(Guid id) => 
        await context.TaskCategories.FindAsync(id);
}