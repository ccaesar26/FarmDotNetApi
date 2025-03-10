using FarmerTasksService.Models.Entities;

namespace FarmerTasksService.Repositories;

public interface ITaskCommentRepository
{
    ValueTask<TaskComment> AddAsync(TaskComment comment);
    ValueTask<List<TaskComment>> GetByTaskIdAsync(Guid taskId);
}