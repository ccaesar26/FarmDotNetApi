namespace ReportsService.Models.Dtos;

public record AddCommentDto(
    string CommentText,
    Guid? ParentCommentId // For replying to a specific comment
    // ReportId and UserId will be added in the service/controller
);