using System.Text.Json.Serialization;

namespace ClimateIndicesService.Models.VitoResponses;

public class VitoGetFeatureInfoResponse
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("features")]
    public List<VitoFeature>? Features { get; set; }
}