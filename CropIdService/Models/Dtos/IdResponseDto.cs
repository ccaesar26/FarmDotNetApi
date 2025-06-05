using System.Text.Json.Serialization;

namespace CropIdService.Models.Dtos;

public record IdResponseDto(
    bool IsPlant,
    SuggestionDto[] CropSuggestions,
    SuggestionDto[] DiseaseSuggestions
);

public record SuggestionDto(
    string Name,
    double Probability,
    string ScientificName
);