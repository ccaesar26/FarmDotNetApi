namespace WeatherService.Models.Dtos;

public record WeatherResponse(
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
    string LottieAnimationName,
    string LottieAnimation,
    string Description
);