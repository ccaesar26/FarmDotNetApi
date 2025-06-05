namespace Shared.Models.Events;

public record TaskCreatedEvent(Guid TaskId, Guid FarmId, Guid CreatedByUserId, DateTime CreatedAt);

public record TaskUpdatedEvent(Guid TaskId);
public record TaskAssignedEvent(Guid TaskId, Guid UserId);
public record TaskUnassignedEvent(Guid TaskId, Guid userId);
public record TaskDeletedEvent(Guid TaskId);
public record TaskCommentAddedEvent(Guid CommentId, Guid TaskId);
public record TaskStatusUpdatedEvent(Guid TaskId, string Status);