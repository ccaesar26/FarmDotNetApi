using FarmerTasksService.Models.Dtos;
using FarmerTasksService.Models.Entities;

namespace FarmerTasksService.Extensions;

public static class TaskCommentExtensions
{
    public static TaskCommentDto ToDto(this TaskComment taskComment)
    {
        return new TaskCommentDto(
            taskComment.Id,
            taskComment.TaskId,
            taskComment.UserId,
            taskComment.CreatedAt,
            taskComment.Comment
        );
    }

    public static TaskComment ToEntity(this CreateTaskCommentDto createTaskCommentDto)
    {
        return new TaskComment
        {
            TaskId = createTaskCommentDto.TaskId,
            UserId = createTaskCommentDto.UserId,
            Comment = createTaskCommentDto.Comment,
            CreatedAt = DateTime.UtcNow
        };
    }
}