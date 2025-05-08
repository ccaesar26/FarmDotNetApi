using ReportsService.Models.Dtos;
using ReportsService.Models.Entities;
using ReportsService.Models.Enums;

namespace ReportsService.MappingExtensions;

public static class ReportMappingExtensions
{
    public static ReportDto ToDto(this Report report)
    {
        if (report == null) // Basic null check
        {
            // Consider logging this or throwing a specific mapping exception
            throw new ArgumentNullException(nameof(report), "Cannot map a null Report entity to ReportDto.");
        }

        return new ReportDto(
            report.Id,
            report.Title,
            report.Description,
            report.ImageData is not null
                ? Convert.ToBase64String(report.ImageData) // Convert byte array to Base64 string
                : null,
            report.ImageMimeType,
            report.Status,
            report.FieldId,
            report.FarmId,
            report.CreatedByUserId,
            report.CreatedAt,
            report.UpdatedAt,
            report.Comments?.Count ?? 0 // Safely count comments (handle null collection)
        );
    }
}