using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfigurationService.Models;

public class ConfigSetting
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }
    
    [Required]
    public required string Key { get; init; }
    
    [Required]
    public required string EncryptedValue { get; init; }
}