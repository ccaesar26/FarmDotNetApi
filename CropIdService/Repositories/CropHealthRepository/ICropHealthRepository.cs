using CropIdService.Models.CropHealthApi;

namespace CropIdService.Repositories.CropHealthRepository;

public interface ICropHealthRepository
{
    /// <summary>
    /// Identifies crop health issues from a list of base64 encoded images.
    /// </summary>
    /// <param name="base64Images">A list of base64 encoded image strings.</param>
    /// <param name="latitude">Optional latitude for the identification.</param>
    /// <param name="longitude">Optional longitude for the identification.</param>
    /// <param name="datetime">Optional datetime for the identification (ISO 8601 string).</param>
    /// <returns>The IdentificationResponse from the Crop Health API, or null if an error occurs.</returns>
    ValueTask<IdentificationResponse?> IdentifyAsync(
        List<string> base64Images,
        double? latitude = null,
        double? longitude = null,
        string? datetime = null
    );
}