using System.Text.Json.Serialization;

namespace ClimateIndicesService.Models.VitoResponses;

public class VitoFeature
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("properties")]
    public VitoFeatureProperties? Properties { get; set; }
}