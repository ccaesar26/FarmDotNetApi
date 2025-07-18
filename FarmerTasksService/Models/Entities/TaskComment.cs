﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmerTasksService.Models.Entities;

public class TaskComment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }

    [Required]
    public required Guid TaskId { get; init; }
    [ForeignKey(nameof(TaskId))]
    public TaskItem Task { get; init; } = null!;

    [Required]
    public required Guid UserId { get; init; } // ID of the user who created the comment

    [Required]
    public required DateTime CreatedAt { get; init; }

    [Required, MaxLength(500)] // Adjust max length as needed
    public required string Comment { get; init; }
}