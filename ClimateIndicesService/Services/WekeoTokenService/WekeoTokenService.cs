using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ClimateIndicesService.Models.Dtos;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace ClimateIndicesService.Services.WekeoTokenService;

public class WekeoTokenService(
    IHttpClientFactory httpClientFactory,
    IOptions<WekeoApiSettings> settings,
    IMemoryCache cache,
    ILogger<WekeoTokenService> logger)
    : IWekeoTokenService
{
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("WekeoApiClient");
        private readonly WekeoApiSettings _settings = settings.Value;
        private static readonly SemaphoreSlim Semaphore = new(1, 1);
        private const string TokenCacheKey = "WekeoAccessToken";

        public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            if (cache.TryGetValue(TokenCacheKey, out string? token) && !string.IsNullOrEmpty(token))
            {
                return token;
            }

            await Semaphore.WaitAsync(cancellationToken);
            try
            {
                if (cache.TryGetValue(TokenCacheKey, out token) && !string.IsNullOrEmpty(token))
                {
                    return token; // Double-check if fetched by another thread
                }

                logger.LogInformation("Requesting new WEkEO access token.");
                var requestBody = new { username = _settings.Username, password = _settings.Password };
                var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_settings.BaseUrl}/gettoken", content, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    logger.LogError("Failed to retrieve WEkEO access token. Status: {StatusCode}, Response: {ErrorContent}", response.StatusCode, errorContent);
                    throw new ApplicationException($"Failed to retrieve WEkEO access token. Status: {response.StatusCode}");
                }

                var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                var tokenResponse = await JsonSerializer.DeserializeAsync<WekeoTokenResponse>(responseStream, cancellationToken: cancellationToken);

                if (string.IsNullOrEmpty(tokenResponse?.AccessToken))
                {
                    logger.LogError("WEkEO access token response was empty or invalid.");
                    throw new ApplicationException("Failed to retrieve WEkEO access token (empty token).");
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(tokenResponse.ExpiresIn > 60 ? tokenResponse.ExpiresIn - 60 : tokenResponse.ExpiresIn * 0.9)); // Cache for slightly less than expiry
                
                cache.Set(TokenCacheKey, tokenResponse.AccessToken, cacheEntryOptions);
                logger.LogInformation("Successfully obtained and cached WEkEO access token.");
                return tokenResponse.AccessToken;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception while getting WEkEO access token.");
                throw; // Re-throw to allow controller to handle
            }
            finally
            {
                Semaphore.Release();
            }
        }
    }