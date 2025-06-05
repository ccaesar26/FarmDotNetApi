namespace CropIdService.Models.Dtos;

public record IdRequestDto(
    string Name,
    double Longitude,
    double Latitude,
    string FieldName,
    string ImageBase64Data,
    DateTime Datetime
);