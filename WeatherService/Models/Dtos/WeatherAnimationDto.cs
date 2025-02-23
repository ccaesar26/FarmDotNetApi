namespace WeatherService.Models.Dtos;

public record WeatherAnimationDto(
    Guid id,
    string filename,
    string filenamedark,
    List<int> codes
);