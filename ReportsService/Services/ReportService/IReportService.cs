using ReportsService.Models.Dtos;
using ReportsService.Models.Enums;

namespace ReportsService.Services.ReportService;

public interface IReportService
{
    ValueTask<Guid> CreateReportAsync(CreateReportDto dto, Guid farmId, Guid createdByUserId);
    ValueTask<ReportDto?> GetReportByIdAsync(Guid id);
    ValueTask<List<ReportDto>> GetReportsAsync(Guid farmId, ReportStatus? status, int pageNumber, int pageSize);
    ValueTask<List<ReportDto>> GetMyReportsAsync(Guid userId, Guid farmId); // Get reports created by the user
    ValueTask UpdateReportStatusAsync(Guid reportId, ReportStatus newStatus, Guid updatedByUserId);
    ValueTask DeleteReportAsync(Guid reportId, Guid deletedByUserId); // Pass user ID for authorization/auditing
    ValueTask<Guid> AddCommentAsync(AddCommentDto dto, Guid reportId, Guid userId);
    ValueTask<List<CommentDto>> GetCommentsAsync(Guid reportId);
    // Potentially add methods for updating/deleting comments, etc.
}