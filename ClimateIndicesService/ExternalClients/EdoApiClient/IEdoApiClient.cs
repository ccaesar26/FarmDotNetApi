namespace ClimateIndicesService.ExternalClients.EdoApiClient;

public interface IEdoApiClient
{
    ValueTask<Stream> GetDroughtDataAsync(DateTime? time);
}