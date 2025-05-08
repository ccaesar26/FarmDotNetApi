using ReportsService.Models.Dtos;
using ReportsService.Models.Entities;

namespace ReportsService.MappingExtensions;

public static class ReportCommentMappingExtensions
{
    public static CommentDto ToDto(this ReportComment comment)
    {
        if (comment == null)
        {
            throw new ArgumentNullException(nameof(comment), "Cannot map a null ReportComment entity to CommentDto.");
        }

        return new CommentDto(
            comment.Id,
            comment.ReportId,
            comment.ParentCommentId,
            comment.UserId,
            comment.CommentText,
            comment.CreatedAt
            // If building the tree structure in the service:
            // ChildComments = comment.ChildComments?.Select(c => c.ToDto()).ToList() ?? new List<CommentDto>()
        );
    }
}