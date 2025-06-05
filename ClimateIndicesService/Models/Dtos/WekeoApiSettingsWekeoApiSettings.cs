namespace ClimateIndicesService.Models.Dtos;

public class WekeoApiSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string DatasetId_NDVI { get; set; } = string.Empty;
    public string NdviProductType { get; set; } = string.Empty;
}