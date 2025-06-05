namespace ClimateIndicesService.Models.Dtos;

public class NdviMapLayerDetails
{
    public string WmsBaseUrl { get; set; }
    public string LayerName { get; set; }
    public string Style { get; set; }
    public string Format { get; set; }
    public string Crs { get; set; } // The CRS client should use for WMS requests
    public List<string> AvailableDates { get; set; } // ISO 8601 "YYYY-MM-DD"
    public string MinAvailableDateInRequestRange { get; set; } // YYYY-MM-DD
    public string MaxAvailableDateInRequestRange { get; set; } // YYYY-MM-DD
    public List<double>? InitialBoundingBox { get; set; } // Optional: [minLon, minLat, maxLon, maxLat] for map view
}