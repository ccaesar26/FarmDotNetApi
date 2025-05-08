using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportsService.Models.Entities;

public class ReportComment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public Guid ReportId { get; set; } // Foreign key to the main Report
    [ForeignKey(nameof(ReportId))]
    public Report Report { get; set; } = null!;

    public Guid? ParentCommentId { get; set; } // Foreign key for self-referencing (threading)
    [ForeignKey(nameof(ParentCommentId))]
    public ReportComment? ParentComment { get; set; }

    [Required]
    public Guid UserId { get; set; } // ID of the user who wrote the comment

    [Required, MaxLength(1000)]
    public string CommentText { get; set; } = null!;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property for replies to this comment
    public ICollection<ReportComment> ChildComments { get; set; } = new List<ReportComment>();
}