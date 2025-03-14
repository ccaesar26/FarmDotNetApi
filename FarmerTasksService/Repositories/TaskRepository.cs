using FarmerTasksService.Data;
using FarmerTasksService.Models.Dtos;
using FarmerTasksService.Models.Entities;
using FarmerTasksService.Models.Enums;
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
            .Include(ta => ta.TaskAssignments)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async ValueTask<List<TaskItem>> GetAllAsync(Guid farmId, TaskFilterDto? filter = null, int pageNumber = 1,
        int pageSize = int.MaxValue)
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

        if (filter.DueDateStart.HasValue)
        {
            query = query.Where(t => t.DueDate >= filter.DueDateStart);
        }

        if (filter.DueDateEnd.HasValue)
        {
            query = query.Where(t => t.DueDate <= filter.DueDateEnd);
        }

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(t => t.Category)
            .Include(ta => ta.TaskAssignments)
            .ToListAsync();
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

    public async ValueTask<IEnumerable<TaskItem>> GetByAssignedUserAsync(Guid userId, int pageNumber, int pageSize) =>
        await context.TaskAssignments
            .Where(ta => ta.UserId == userId)
            .Include(ta => ta.Task).ThenInclude(t => t.Category)
            .Select(ta => ta.Task)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async ValueTask AssignUsersToTaskAsync(Guid taskId, List<Guid> userIds)
    {
        var task = await GetByIdAsync(taskId);

        if (task == null)
        {
            throw new KeyNotFoundException($"Task with id {taskId} not found");
        }

        var taskAssignments = userIds.Select(userId => new TaskAssignment
        {
            TaskId = taskId,
            UserId = userId
        });

        await context.TaskAssignments.AddRangeAsync(taskAssignments);
        await context.SaveChangesAsync();
    }

    public async ValueTask UnassignUsersFromTaskAsync(Guid taskId, List<Guid> userIds)
    {
        var task = await GetByIdAsync(taskId);

        if (task == null)
        {
            throw new KeyNotFoundException($"Task with id {taskId} not found");
        }

        var taskAssignments = await context.TaskAssignments
            .Where(ta => ta.TaskId == taskId && userIds.Contains(ta.UserId))
            .ToListAsync();

        context.TaskAssignments.RemoveRange(taskAssignments);
        await context.SaveChangesAsync();
    }

    public async ValueTask<IEnumerable<TaskItem>> GetAllNotGeneratedRecurringAsync() =>
        await context.Tasks
            .Where(t => t.Recurrence != RecurrenceType.None &&
                        (t.LastGeneratedDate == null || t.LastGeneratedDate < DateTime.UtcNow))
            // .Where(t => t.Recurrence != RecurrenceType.None && t.RecurrenceEndDate.HasValue &&
            //             t.RecurrenceEndDate > DateTime.UtcNow)
            .ToListAsync();
}