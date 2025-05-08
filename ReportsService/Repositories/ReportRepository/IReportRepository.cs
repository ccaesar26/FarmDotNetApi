using ReportsService.Models.Entities;
using ReportsService.Models.Enums;

namespace ReportsService.Repositories.ReportRepository;

public interface IReportRepository
{
    ValueTask<Report> AddAsync(Report report);
    ValueTask<Report?> GetByIdAsync(Guid id);
    ValueTask<List<Report>> GetReportsAsync(Guid farmId, ReportStatus? status, int pageNumber, int pageSize);
    ValueTask<List<Report>> GetReportsByUserIdAsync(Guid userId, Guid farmId); 
    ValueTask UpdateAsync(Report report);
    ValueTask DeleteAsync(Guid id);
}