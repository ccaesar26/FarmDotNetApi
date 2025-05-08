using Microsoft.EntityFrameworkCore;
using ReportsService.Data;
using ReportsService.Models.Entities;

namespace ReportsService.Repositories.ReportCommentRepository;

public class ReportCommentRepository(ReportsDbContext dbContext) : IReportCommentRepository
{
    public async ValueTask<ReportComment> AddAsync(ReportComment comment)
    {
        dbContext.ReportComments.Add(comment);
        await dbContext.SaveChangesAsync();
        return comment;
    }

    public async ValueTask<ReportComment?> GetByIdAsync(Guid id)
    {
        // Include parent and child comments if needed
        return await dbContext.ReportComments
            .Include(c => c.ParentComment)
            .Include(c => c.ChildComments)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async ValueTask<List<ReportComment>> GetCommentsByReportIdAsync(Guid reportId)
    {
        // Include child comments for threading
        return await dbContext.ReportComments
            .Where(c => c.ReportId == reportId)
            .Include(c => c.ChildComments) // Include replies if needed
            .ToListAsync();
    }

    public async ValueTask DeleteAsync(Guid id)
    {
        var comment = await GetByIdAsync(id);
        if (comment != null)
        {
            dbContext.ReportComments.Remove(comment);
            await dbContext.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException($"Comment with ID {id} not found.");
        }
    }
}