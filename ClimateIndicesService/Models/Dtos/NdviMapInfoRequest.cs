using System.ComponentModel.DataAnnotations;

namespace ClimateIndicesService.Models.Dtos;

// Models/NdviMapInfoRequest.cs
public class NdviMapInfoRequest
{
    [Required]
    public double MinLongitude { get; set; }
    [Required]
    public double MinLatitude { get; set; }
    [Required]
    public double MaxLongitude { get; set; }
    [Required]
    public double MaxLatitude { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}

// Models/NdviMapLayerDetails.cs