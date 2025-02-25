namespace FieldsService.Models.Dtos;

public record GeocodingResponse(List<GeocodingResult> Results);

public record GeocodingResult(string Name, double Lat, double Lon, string Country);
