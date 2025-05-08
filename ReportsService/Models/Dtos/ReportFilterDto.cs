using ReportsService.Models.Enums;

namespace ReportsService.Models.Dtos;

public record ReportFilterDto(
    ReportStatus? Status,
    Guid? CreatedByUserId, // Filter by creator
    Guid? FieldId // Filter by field
    // Add other potential filters like date ranges, etc.
);