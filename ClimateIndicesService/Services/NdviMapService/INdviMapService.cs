using ClimateIndicesService.Models.Dtos;

namespace ClimateIndicesService.Services.NdviMapService;

public interface INdviMapService
{
    Task<NdviMapLayerDetails> GetMapLayerDetailsAsync(NdviMapInfoRequest request);
}