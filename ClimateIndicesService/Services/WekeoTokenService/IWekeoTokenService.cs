namespace ClimateIndicesService.Services.WekeoTokenService;

public interface IWekeoTokenService
{
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}