﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserProfileService.Attributes;
using UserProfileService.Models.Dtos;

namespace UserProfileService.Models.Entities;

public class UserProfile
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }
    
    [Required]
    [MaxLength(120)]
    public required string Name { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    [PastDate(ErrorMessage = "Date of birth must be in the past.")]
    public required DateOnly DateOfBirth { get; set; }
    
    [Required]
    [MaxLength(20)]
    [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'")]
    public required string Gender { get; set; }
    
    public Guid? UserId { get; set; }
    
    public ICollection<ProfileAttribute> ProfileAttributes { get; set; } = new List<ProfileAttribute>();
}