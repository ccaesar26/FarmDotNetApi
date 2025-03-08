namespace Shared.Models.Events;

public record UserRoleAssignedEvent(Guid UserId, string Role);