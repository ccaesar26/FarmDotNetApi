namespace NotificationService.Models.Dtos;

public record NotificationDto(
    Guid Id, // Unique identifier for the notification
    string NotificationType, // e.g., "NewReport", "TaskAssigned", "CommentAdded"
    Guid SourceEntityId, // e.g., ReportId, TaskId
    Guid TriggeringUserId, 
    DateTime Timestamp,
    Guid TargetFarmId, // To route to the correct farm group
    Guid? TargetUserId, // Optional if the notification is user-specific
    bool IsRead
);