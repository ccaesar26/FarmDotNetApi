namespace Shared.Models.Events;

public record UserProfileCreatedEvent(
    Guid UserId,
    Guid UserProfileId
);