using ReportsService.Models.Enums;

namespace ReportsService.Models.Dtos;

public record ReportDto(
    Guid Id,
    string Title,
    string? Description,
    string? ImageData,
    string? ImageMimeType,
    ReportStatus Status,
    Guid? FieldId,
    Guid FarmId,
    Guid CreatedByUserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int CommentCount // Added for convenience
);