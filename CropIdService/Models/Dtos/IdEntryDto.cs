namespace CropIdService.Models.Dtos;

public record IdEntryDto(
    string Id,
    string Name,
    double Longitude,
    double Latitude,
    string FieldName,
    string ImageBase64Data,
    DateTime Datetime,
    bool IsPlant,
    IList<SuggestionEntryDto> Suggestions
);