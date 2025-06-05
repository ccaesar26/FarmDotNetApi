namespace ReportsService.Models.Events;

public record ReportCreatedSignalREvent(
    Guid ReportId,
    Guid FarmId,
    Guid CreatedByUserId
);