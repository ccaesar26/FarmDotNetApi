using CropIdService.Models.CropHealthApi;
using CropIdService.Models.Dtos;
using CropIdService.Models.Entities;
using CropIdService.Models.Enums;

namespace CropIdService.Mapping.Extensions;

public static class CropIdExtensions
{
    public static IdResponseDto ToDto(this IdentificationResponse response)
    {
        var isPlant = response.Result.IsPlant.Probability > response.Result.IsPlant.Threshold;
        var cropSuggestions = response.Result.Crop.Suggestions
            .Select(s => new SuggestionDto(s.Name, s.Probability, s.ScientificName))
            .ToArray();
        var diseaseSuggestions = response.Result.Disease.Suggestions
            .Select(s => new SuggestionDto(s.Name, s.Probability, s.ScientificName))
            .ToArray();

        return new IdResponseDto(isPlant, cropSuggestions, diseaseSuggestions);
    }

    public static IdEntry ToEntity(
        this IdentificationResponse response,
        Guid farmId,
        string name,
        string fieldName,
        string imageBase64Data
    ) => new()
    {
        Name = name,
        Longitude = response.Input.Longitude,
        Latitude = response.Input.Latitude,
        FieldName = fieldName,
        FarmId = farmId,
        ImageBase64Data = imageBase64Data,
        Datetime = DateTime.UtcNow,
        IsPlant = response.Result.IsPlant.Probability > response.Result.IsPlant.Threshold,
        Suggestions = response.Result.Crop.Suggestions
            .Select(s => new SuggestionEntry
            {
                Type = SuggestionType.Crop,
                Name = s.Name,
                Probability = s.Probability,
                ScientificName = s.ScientificName
            })
            .Concat(response.Result.Disease.Suggestions
                .Select(s => new SuggestionEntry
                {
                    Type = SuggestionType.Disease,
                    Name = s.Name,
                    Probability = s.Probability,
                    ScientificName = s.ScientificName
                })).ToList()
    };

    public static IdEntryDto ToDto(this IdEntry entry)
    {
        return new IdEntryDto
        (
            entry.Id.ToString(),
            entry.Name,
            entry.Longitude,
            entry.Latitude,
            entry.FieldName,
            entry.ImageBase64Data,
            entry.Datetime,
            entry.IsPlant,
            entry.Suggestions.Select(s => new SuggestionEntryDto(
                s.Id.ToString(),
                s.Type == SuggestionType.Crop ? "Crop" : "Disease",
                s.Name,
                s.Probability,
                s.ScientificName
            )).ToArray()
        );
    }
}