using FieldsService.Models.Dtos;

namespace FieldsService.Services.GeocodingService;

public interface IGeocodingService
{
    public ValueTask<GeocodingResult?> GetCityByCoordinatesAsync(double latitude, double longitude);
}