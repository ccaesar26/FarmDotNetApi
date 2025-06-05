namespace Shared.Models.Events;

public record ReportCreatedEvent(
    Guid ReportId,
    Guid FarmId,
    Guid CreatedByUserId,
    DateTime CreatedAt
);