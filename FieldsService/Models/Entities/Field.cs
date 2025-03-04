using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;

namespace FieldsService.Models.Entities;

public class Field
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }
    
    [Required]
    public Guid FarmId { get; set; }
    
    [Required]
    [MaxLength(120)]
    public required string Name { get; set; }
    
    [Required]
    [JsonConverter(typeof(NetTopologySuite.IO.Converters.GeometryConverter))]
    public required Polygon Boundary { get; set; }
}