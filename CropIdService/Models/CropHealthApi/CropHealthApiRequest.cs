using System.Text.Json.Serialization;

namespace CropIdService.Models.CropHealthApi;

public record CropHealthApiRequest(
    [property: JsonPropertyName("images")] List<string> Images,
    [property: JsonPropertyName("latitude")] double? Latitude = null,
    [property: JsonPropertyName("longitude")] double? Longitude = null,
    [property: JsonPropertyName("datetime")] string? Datetime = null
);