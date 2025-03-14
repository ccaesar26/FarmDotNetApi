namespace FarmerTasksService.Models.Dtos;

public record AssignUsersToTaskRequest(
    List<Guid> UserIds
);