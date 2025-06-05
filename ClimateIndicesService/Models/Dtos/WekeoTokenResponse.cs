using System.Text.Json.Serialization;

namespace ClimateIndicesService.Models.Dtos;

public class WekeoTokenResponse
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; } // Typically 3600 seconds
}