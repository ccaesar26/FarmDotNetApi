namespace Shared.Models.Events;

public record TaskCreatedEvent(Guid TaskId);

public record TaskUpdatedEvent(Guid TaskId);
public record TaskAssignedEvent(Guid TaskId, Guid UserId);
public record TaskUnassignedEvent(Guid TaskId);
public record TaskDeletedEvent(Guid TaskId);
public record TaskCommentAddedEvent(Guid CommentId, Guid TaskId);
public record TaskStatusUpdatedEvent(Guid TaskId, string Status);