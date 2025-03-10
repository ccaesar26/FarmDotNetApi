namespace FarmerTasksService.Models.Dtos;

public record TaskCommentDto(
    Guid Id,
    Guid TaskId,
    Guid UserId,
    DateTime CreatedAt,
    string Comment
);