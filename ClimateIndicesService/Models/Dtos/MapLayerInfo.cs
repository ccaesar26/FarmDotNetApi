namespace ClimateIndicesService.Models.Dtos;

public class MapLayerInfo
{
    public string WmsBaseUrl { get; set; } = string.Empty;
    public string LayerName { get; set; } = string.Empty;
    // public string[]? AvailableTimes { get; set; } // Optional: if fetched from GetCapabilities
}