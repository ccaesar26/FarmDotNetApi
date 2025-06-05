using System.Text.Json.Serialization;

namespace CropIdService.Models.CropHealthApi;

public record IdentificationResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("model_version")] string ModelVersion,
    [property: JsonPropertyName("custom_id")] string? CustomId,
    [property: JsonPropertyName("input")] InputDetails Input,
    [property: JsonPropertyName("result")] IdentificationResult Result,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("sla_compliant_client")] bool SlaCompliantClient,
    [property: JsonPropertyName("sla_compliant_system")] bool SlaCompliantSystem,
    [property: JsonPropertyName("created")] double Created,
    [property: JsonPropertyName("completed")] double Completed
);

public record InputDetails(
    [property: JsonPropertyName("latitude")] double Latitude,
    [property: JsonPropertyName("longitude")] double Longitude,
    [property: JsonPropertyName("images")] List<string> Images,
    [property: JsonPropertyName("datetime")] DateTime Datetime
);

public record IdentificationResult(
    [property: JsonPropertyName("is_plant")] IsPlantResult IsPlant,
    [property: JsonPropertyName("disease")] SuggestionsContainer Disease,
    [property: JsonPropertyName("crop")] SuggestionsContainer Crop
);

public record IsPlantResult(
    [property: JsonPropertyName("probability")] double Probability,
    [property: JsonPropertyName("threshold")] double Threshold,
    [property: JsonPropertyName("binary")] bool Binary
);

public record SuggestionsContainer(
    [property: JsonPropertyName("suggestions")] List<Suggestion> Suggestions
);

public record Suggestion(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("probability")] double Probability,
    [property: JsonPropertyName("details")] SuggestionDetails Details,
    [property: JsonPropertyName("scientific_name")] string ScientificName
);

public record SuggestionDetails(
    [property: JsonPropertyName("language")] string Language,
    [property: JsonPropertyName("entity_id")] string EntityId
);