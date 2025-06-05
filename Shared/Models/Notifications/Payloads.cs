namespace Shared.Models.Notifications;

// Example Payloads
public record NewReportPayload(string ReportTitle, string SubmittedByUsername);
public record TaskAssignedPayload(string TaskTitle, List<string> AssignedToUsernames);