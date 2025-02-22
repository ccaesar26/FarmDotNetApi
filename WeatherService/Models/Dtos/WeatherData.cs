namespace WeatherService.Models.Dtos;

public record WeatherData(
    Coord coord,
    List<Weather> weather,
    string @base,
    Main main,
    int visibility,
    Wind wind,
    Clouds clouds,
    long dt,
    Sys sys,
    int timezone,
    int id,
    string name,
    int cod
);

public record Coord(double lon, double lat);

public record Weather(
    int id,
    string main,
    string description,
    string icon
);

public record Main(
    double temp,
    double feels_like,
    double temp_min,
    double temp_max,
    int pressure,
    int humidity,
    int sea_level,
    int grnd_level
);

public record Wind(
    double speed,
    int deg,
    double gust
);

public record Clouds(int all);

public record Sys(
    int type,
    int id,
    string country,
    long sunrise,
    long sunset
);
