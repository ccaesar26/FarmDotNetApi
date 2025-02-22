namespace WeatherService.Models.Dtos;

public record WeatherRequest(
    string? City,
    double? Latitude,
    double? Longitude
);