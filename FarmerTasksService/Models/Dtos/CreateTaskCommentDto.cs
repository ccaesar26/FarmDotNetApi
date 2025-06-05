namespace FarmerTasksService.Models.Dtos;

public record CreateTaskCommentDto(
    Guid TaskId,
    string Comment
);