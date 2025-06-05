namespace Shared.Models.Events;

public record ReportNewCommentEvent(
    Guid ReportId,
    Guid FarmId,
    Guid CreatedByUserId,
    DateTime CreatedAt
);