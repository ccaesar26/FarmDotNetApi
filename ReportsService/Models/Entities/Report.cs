using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ReportsService.Models.Enums;

namespace ReportsService.Models.Entities;

public class Report
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = null!;

    [MaxLength(2000)] // Increased length for potentially detailed descriptions
    public string? Description { get; set; }

    public byte[]? ImageData { get; set; } // Stores the actual image bytes (nullable if image is optional)

    [MaxLength(100)] // Store the MIME type (e.g., "image/jpeg", "image/png")
    public string? ImageMimeType { get; set; } // Nullable if ImageData is nullable

    [Required]
    public ReportStatus Status { get; set; }

    public Guid? FieldId { get; set; } // Optional: Link to a specific field (ID from another service)

    [Required]
    public Guid FarmId { get; set; } // ID of the farm this report belongs to

    [Required]
    public Guid CreatedByUserId { get; set; } // ID of the user who submitted the report

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } // Track last update time

    // Navigation property for comments related to this report
    public ICollection<ReportComment> Comments { get; set; } = new List<ReportComment>();
}