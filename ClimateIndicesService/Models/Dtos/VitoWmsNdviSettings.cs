namespace ClimateIndicesService.Models.Dtos;

public class VitoWmsNdviSettings
{
    public string BaseUrl { get; set; }
    public string LayerName { get; set; }
    public string DefaultStyle { get; set; }
    public string ImageFormat { get; set; }
    public string Srs { get; set; } // CRS for the WMS GetMap requests
    public string ServiceOverallStartDate { get; set; }
    public string ServiceOverallEndDate { get; set; }
}