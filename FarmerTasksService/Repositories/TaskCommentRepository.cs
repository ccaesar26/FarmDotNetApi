using FarmerTasksService.Data;
using FarmerTasksService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmerTasksService.Repositories;

public class TaskCommentRepository(FarmerTaskDbContext context) : ITaskCommentRepository
{
    public async ValueTask<TaskComment> AddAsync(TaskComment comment)
    {
        context.Add(comment);
        await context.SaveChangesAsync();
        return comment;
    }

    public async ValueTask<List<TaskComment>> GetByTaskIdAsync(Guid taskId) =>
        await context.TaskComments
            .Where(c => c.TaskId == taskId)
            .ToListAsync();
}