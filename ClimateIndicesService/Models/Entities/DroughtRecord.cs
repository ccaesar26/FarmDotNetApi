using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimateIndicesService.Models.Entities;

public class DroughtRecord
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [Column(TypeName = "bytea")]
    public required byte[] RasterData { get; set; } // Will store the raster data as a byte array
    
    [Required]
    public required DateTime Date { get; set; } // Date of the raster data
}