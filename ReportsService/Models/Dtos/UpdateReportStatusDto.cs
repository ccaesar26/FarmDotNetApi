using ReportsService.Models.Enums;

namespace ReportsService.Models.Dtos;

public record UpdateReportStatusDto(
    ReportStatus Status
);