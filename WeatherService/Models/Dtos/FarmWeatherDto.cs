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
)
{
    public WeatherResponse ToWeatherResponse()
    {
        return new WeatherResponse(
            Location,
            Temperature,
            FeelsLike,
            Humidity,
            Pressure,
            WindSpeed,
            WindGust,
            CloudCoverage,
            Sunrise,
            Sunset,
            string.Empty,
            string.Empty
        );
    }
}