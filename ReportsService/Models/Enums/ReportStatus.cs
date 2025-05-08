namespace ReportsService.Models.Enums;

// Enum for Report Status
public enum ReportStatus
{
    Submitted,  // Initial state when created by a worker
    Seen,       // When the manager has viewed the report
    InProgress, // Manager is taking action
    Resolved,   // The issue reported has been addressed
    Closed      // No further action needed, different from Resolved
}