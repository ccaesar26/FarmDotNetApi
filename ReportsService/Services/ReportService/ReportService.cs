using ReportsService.MappingExtensions;
using ReportsService.Models.Dtos;
using ReportsService.Models.Entities;
using ReportsService.Models.Enums;
using ReportsService.Repositories.ReportCommentRepository;
using ReportsService.Repositories.ReportRepository;

namespace ReportsService.Services.ReportService;

public class ReportService(
    IReportRepository reportRepository,
    IReportCommentRepository commentRepository,
    ILogger<ReportService> logger
) : IReportService
{
    public async ValueTask<Guid> CreateReportAsync(CreateReportDto dto, Guid farmId, Guid createdByUserId)
    {
        var report = new Report
        {
            Title = dto.Title,
            Description = dto.Description,
            ImageData = dto.ImageData,
            ImageMimeType = dto.ImageMimeType,
            FieldId = dto.FieldId,
            FarmId = farmId,
            CreatedByUserId = createdByUserId,
            Status = ReportStatus.Submitted, // Initial status
            CreatedAt = DateTime.UtcNow
        };

        var createdReport = await reportRepository.AddAsync(report);
        logger.LogInformation("Report created with ID: {ReportId}", createdReport.Id);
        return createdReport.Id;
    }

    public async ValueTask<ReportDto?> GetReportByIdAsync(Guid id)
    {
        var report = await reportRepository.GetByIdAsync(id);
        return report?.ToDto(); // Assuming you have a ToDto extension method
    }

    public async ValueTask<List<ReportDto>> GetReportsAsync(Guid farmId, ReportStatus? status, int pageNumber,
        int pageSize)
    {
        var reports = await reportRepository.GetReportsAsync(farmId, status, pageNumber, pageSize);
        return reports.Select(r => r.ToDto()).ToList(); // Map to DTOs
    }

    public async ValueTask<List<ReportDto>> GetMyReportsAsync(Guid userId, Guid farmId)
    {
        var reports = await reportRepository.GetReportsByUserIdAsync(userId, farmId);
        return reports.Select(r => r.ToDto()).ToList();
    }

    public async ValueTask UpdateReportStatusAsync(Guid reportId, ReportStatus newStatus, Guid updatedByUserId)
    {
        var report = await reportRepository.GetByIdAsync(reportId);
        if (report == null)
        {
            throw new KeyNotFoundException($"Report with ID {reportId} not found.");
        }

        // Add authorization logic here if needed:
        // Can updatedByUserId change the status of this report?

        report.Status = newStatus;
        // Optionally, add logic for state transitions (e.g., can't go from Closed to InProgress)

        await reportRepository.UpdateAsync(report);
        logger.LogInformation("Report {ReportId} status updated to {Status} by User {UserId}", reportId, newStatus,
            updatedByUserId);
    }

    public async ValueTask DeleteReportAsync(Guid reportId, Guid deletedByUserId)
    {
        var report = await reportRepository.GetByIdAsync(reportId);
        if (report == null)
        {
            logger.LogWarning("Attempted to delete non-existent report {ReportId} by User {UserId}", reportId,
                deletedByUserId);
            return; // Or throw KeyNotFoundException
        }

        // Add authorization check: Can deletedByUserId delete this report?

        await reportRepository.DeleteAsync(reportId);
        logger.LogInformation("Report {ReportId} deleted by User {UserId}", reportId, deletedByUserId);
    }

    public async ValueTask<Guid> AddCommentAsync(AddCommentDto dto, Guid reportId, Guid userId)
    {
        // Ensure the report exists (could be done in repo or here)
        var reportExists = await reportRepository.GetByIdAsync(reportId);
        if (reportExists == null)
        {
            throw new KeyNotFoundException($"Cannot add comment to non-existent report with ID {reportId}.");
        }

        var comment = new ReportComment
        {
            ReportId = reportId,
            UserId = userId,
            CommentText = dto.CommentText,
            ParentCommentId = dto.ParentCommentId, // Handle replies
            CreatedAt = DateTime.UtcNow
        };

        var createdComment = await commentRepository.AddAsync(comment);
        logger.LogInformation("Comment {CommentId} added to Report {ReportId} by User {UserId}", createdComment.Id,
            reportId, userId);
        return createdComment.Id;
    }

    public async ValueTask<List<CommentDto>> GetCommentsAsync(Guid reportId)
    {
        var comments = await commentRepository.GetCommentsByReportIdAsync(reportId);
        // You might want to structure the comments into a thread/tree here before returning
        return comments.Select(c => c.ToDto()).ToList(); // Map to DTOs
    }
}