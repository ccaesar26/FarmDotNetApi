namespace ReportsService.Models.Dtos;

public record CreateReportDto(
    string Title,
    string? Description,
    byte[]? ImageData,
    string? ImageMimeType,
    Guid? FieldId
);