namespace WeatherService.Models.Dtos;

public record DailyForecastWithAnimationResponse(
    string Date,
    double DayTemperature,
    double MinTemperature,
    double MaxTemperature,
    int Humidity,
    int Pressure,
    double WindSpeed,
    double WindGust,
    int CloudCoverage,
    double PrecipitationProbability,
    double? Rain,
    double? Snow,
    string LottieAnimationName,
    string LottieAnimation
);