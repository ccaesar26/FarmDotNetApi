namespace ReportsService.Models.Events;

public record ReportCreatedEvent(
    Guid ReportId,
    Guid FarmId,
    Guid CreatedByUserId
);