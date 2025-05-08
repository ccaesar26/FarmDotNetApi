using Microsoft.EntityFrameworkCore;
using ReportsService.Data;
using ReportsService.Models.Entities;
using ReportsService.Models.Enums;

namespace ReportsService.Repositories.ReportRepository;

public class ReportRepository(ReportsDbContext dbContext) :IReportRepository
{
    public async ValueTask<Report> AddAsync(Report report)
    {
        dbContext.Reports.Add(report);
        await dbContext.SaveChangesAsync();
        return report;
    }

    public async ValueTask<Report?> GetByIdAsync(Guid id)
    {
        // Include comments and potentially the parent/child comments for detail view
        return await dbContext.Reports
            .Include(r => r.Comments)
            .ThenInclude(c => c.ChildComments) // Include replies if needed immediately
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async ValueTask<List<Report>> GetReportsAsync(Guid farmId, ReportStatus? status, int pageNumber, int pageSize)
    {
        var query = dbContext.Reports.Where(r => r.FarmId == farmId);

        if (status.HasValue)
        {
            query = query.Where(r => r.Status == status.Value);
        }

        // Apply pagination
        return await query
            .OrderByDescending(r => r.CreatedAt) // Example sorting
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(r => r.Comments) // Include comment count or comments if needed for list view
            .ToListAsync();
    }

    public async ValueTask<List<Report>> GetReportsByUserIdAsync(Guid userId, Guid farmId)
    {
        var query = dbContext.Reports
            .Where(r => r.CreatedByUserId == userId && r.FarmId == farmId);

        return await query
            .OrderByDescending(r => r.CreatedAt)
            .Include(r => r.Comments)
            .ToListAsync();
    }

    public async ValueTask UpdateAsync(Report report)
    {
        report.UpdatedAt = DateTime.UtcNow; // Update timestamp
        dbContext.Reports.Update(report);
        await dbContext.SaveChangesAsync();
    }

    public async ValueTask DeleteAsync(Guid id)
    {
        var report = await dbContext.Reports.FindAsync(id);
        if (report != null)
        {
            dbContext.Reports.Remove(report);
            await dbContext.SaveChangesAsync();
        }
    }
}