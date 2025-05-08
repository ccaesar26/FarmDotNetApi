using ReportsService.Models.Entities;

namespace ReportsService.Repositories.ReportCommentRepository;

public interface IReportCommentRepository
{
    ValueTask<ReportComment> AddAsync(ReportComment comment);
    ValueTask<ReportComment?> GetByIdAsync(Guid id);
    ValueTask<List<ReportComment>> GetCommentsByReportIdAsync(Guid reportId);
    ValueTask DeleteAsync(Guid id);
}