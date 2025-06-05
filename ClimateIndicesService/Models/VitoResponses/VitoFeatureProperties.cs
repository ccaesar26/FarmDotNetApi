using System.Text.Json.Serialization;

namespace ClimateIndicesService.Models.VitoResponses;

public class VitoFeatureProperties
{
    // The property name containing the NDVI value needs to be confirmed.
    // Common names are "GRAY_INDEX", "value", "pixel_value", etc.
    // Assuming it's "GRAY_INDEX" and it's the scaled INT16 value.
    [JsonPropertyName("GRAY_INDEX")]
    public int? GrayIndex { get; set; } // If it's an integer
        
    // Or if it's directly a double
    // [JsonPropertyName("value")]
    // public double? Value { get; set; } 
}