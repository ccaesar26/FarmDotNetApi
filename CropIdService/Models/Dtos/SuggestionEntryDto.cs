namespace CropIdService.Models.Dtos;

public record SuggestionEntryDto(
    string Id,
    string Type,
    string Name,
    double Probability,
    string ScientificName
);