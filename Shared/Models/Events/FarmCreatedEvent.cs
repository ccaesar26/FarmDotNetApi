namespace Shared.Models.Events;

public record FarmCreatedEvent(
    Guid UserId,
    Guid FarmId
);