namespace WeatherService.Models.Dtos;

public record WeatherConditionDto(
    Guid id,
    int code,
    string description
);