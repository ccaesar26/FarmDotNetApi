namespace WeatherService.Models.Dtos;

public record FarmWeatherDto(
    string Location,
    double Temperature,
    double FeelsLike,
    double Humidity,
    double Pressure,
    double WindSpeed,
    double WindGust,
    int CloudCoverage,
    TimeOnly Sunrise,
    TimeOnly Sunset,
    int Code
);