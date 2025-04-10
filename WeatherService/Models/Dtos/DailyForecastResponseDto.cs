// ReSharper disable InconsistentNaming
namespace WeatherService.Models.Dtos;

public record DailyForecastResponseDto(
    CityDto city,
    string cod,
    double message,
    int cnt,
    List<DailyForecastItemDto> list
);

public record CityDto(
    int id,
    string name,
    CoordinatesDto coord,
    string country,
    int population,
    int timezone
);

public record CoordinatesDto(
    double lon,
    double lat
);

public record DailyForecastItemDto(
    long dt,
    long sunrise,
    long sunset,
    TemperatureDto temp,
    FeelsLikeDto feels_like,
    int pressure,
    int humidity,
    List<WeatherDescriptionDto> weather,
    double speed,
    int deg,
    double gust,
    int clouds,
    double pop,
    double? rain,
    double? snow
);

public record TemperatureDto(
    double day,
    double min,
    double max,
    double night,
    double eve,
    double morn
);

public record FeelsLikeDto(
    double day,
    double night,
    double eve,
    double morn
);

public record WeatherDescriptionDto(
    int id,
    string main,
    string description,
    string icon
);
