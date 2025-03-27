using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CropCatalogService.Model.Entities;

public class CropCatalogEntry
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public required string BinomialName { get; set; }
    
    [Required]
    public required bool IsPerennial { get; set; }
    
    public int? DaysToFirstHarvest { get; set; }
    public int? DaysToLastHarvest { get; set; }
    
    public int? AverageDaysToHarvest => (DaysToFirstHarvest + DaysToLastHarvest) / 2;
    
    public int? MinMonthsToBearFruit { get; set; }
    public int? MaxMonthsToBearFruit { get; set; }
    
    public int? AverageMonthsToBearFruit => (MinMonthsToBearFruit + MaxMonthsToBearFruit) / 2;
    
    public DateOnly? HarvestSeasonStart { get; set; }
    public DateOnly? HarvestSeasonEnd { get; set; }
    
    public string? Description { get; set; }
    public string? WikipediaLink { get; set; }
    public string? ImageLink { get; set; }
    
    public string? SunRequirements { get; set; }
    public string? SowingMethod { get; set; }
}