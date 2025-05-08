namespace ReportsService.Models.Dtos;

public record CommentDto(
    Guid Id,
    Guid ReportId,
    Guid? ParentCommentId, // For threading
    Guid UserId,
    string CommentText,
    DateTime CreatedAt
    // You might want to include ChildComments here if you build the tree in the service
    // List<CommentDto>? ChildComments = null
);