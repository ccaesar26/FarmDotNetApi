using System.ComponentModel.DataAnnotations;

namespace UserProfileService.Attributes;

public class PastDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateOnly date && date > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return new ValidationResult(ErrorMessage ?? "The date must be in the past.");
        }
        return ValidationResult.Success;
    }
}