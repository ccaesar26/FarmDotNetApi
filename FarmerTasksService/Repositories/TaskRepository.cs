using FarmerTasksService.Data;
using FarmerTasksService.Models.Dtos;
using FarmerTasksService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmerTasksService.Repositories;

public class TaskRepository(FarmerTaskDbContext context) : ITaskRepository
{
    public async ValueTask<TaskItem> AddAsync(TaskItem task)
    {
        context.Tasks.Add(task);
        await context.SaveChangesAsync();
        return task;
    }

    public async ValueTask<TaskItem?> GetByIdAsync(Guid id)
    {
        return await context.Tasks
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async ValueTask<List<TaskItem>> GetAllAsync(TaskFilterDto? filter = null)
    {
        var tasks = context.Tasks
            .Include(t => t.Category);

        if (filter == null) return await tasks.ToListAsync();

        var query = tasks.AsQueryable();

        if (filter.Status.HasValue)
        {
            query = query.Where(t => t.Status == filter.Status);
        }

        if (filter.Priority.HasValue)
        {
            query = query.Where(t => t.Priority == filter.Priority);
        }

        if (filter.CategoryId.HasValue)
        {
            query = query.Where(t => t.CategoryId == filter.CategoryId);
        }

        if (filter.AssignedUserId.HasValue)
        {
            query = query.Where(t => t.AssignedUserId == filter.AssignedUserId);
        }

        if (filter.DueDateStart.HasValue)
        {
            query = query.Where(t => t.DueDate >= filter.DueDateStart);
        }

        if (filter.DueDateEnd.HasValue)
        {
            query = query.Where(t => t.DueDate <= filter.DueDateEnd);
        }

        return await query.ToListAsync();
    }

    public async ValueTask UpdateAsync(TaskItem task)
    {
        context.Tasks.Update(task);
        await context.SaveChangesAsync();
    }

    public async ValueTask DeleteAsync(Guid id)
    {
        var task = await GetByIdAsync(id);
        if (task == null) return;

        context.Tasks.Remove(task);
        await context.SaveChangesAsync();
    }

    public async ValueTask<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId) =>
        await context.Tasks
            .Include(t => t.Category)
            .Where(t => t.AssignedUserId == userId)
            .ToListAsync();
}