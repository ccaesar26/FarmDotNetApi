namespace FarmerTasksService.Models.Dtos;

public record UnassignUsersFromTaskRequest(
    List<Guid> UserIds
);