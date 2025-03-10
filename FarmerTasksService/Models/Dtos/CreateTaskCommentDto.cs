namespace FarmerTasksService.Models.Dtos;

public record CreateTaskCommentDto(
    Guid TaskId,
    Guid UserId,
    string Comment
);