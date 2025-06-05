using CropIdService.Mapping.Extensions;
using CropIdService.Models.Dtos;
using CropIdService.Models.Entities;
using CropIdService.Repositories.CropHealthRepository;
using CropIdService.Repositories.IdEntryRepository;

namespace CropIdService.Services.CropHealthService;

public class CropHealthService(
    ICropHealthRepository cropApiRepository,
    IIdEntryRepository idEntryRepository,
    ILogger<CropHealthService> logger
) : ICropHealthService
{
    public async ValueTask<IdResponseDto?> IdentifyCropHealthAsync(IdRequestDto requestDto, Guid farmId)
    {
        // 1. Prepare the request data
        var imagesToApi = new List<string> { requestDto.ImageBase64Data };
        
        var utcDatetime = requestDto.Datetime.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(requestDto.Datetime, DateTimeKind.Local).ToUniversalTime() // Assume local if unspecified, then convert
            : requestDto.Datetime.ToUniversalTime();
        var isoDateTime = utcDatetime.ToString("yyyy-MM-ddTHH:mm:ss");
        
        // 2. Call the external API
        logger.LogInformation("Calling Crop Health API for request: {Name}", requestDto.Name);
        var apiResponse = await cropApiRepository.IdentifyAsync(
            imagesToApi,
            requestDto.Latitude, // Assuming your API takes these as doubles
            requestDto.Longitude,
            isoDateTime
        );

        if (apiResponse == null)
        {
            logger.LogError("Failed to get a response from Crop Health API for request: {Name}", requestDto.Name);
            return null; // Or throw a custom exception
        }

        logger.LogInformation("Received response from Crop Health API. Status: {Status}", apiResponse.Status);

        // 3. Map the API response to your IdEntry entity
        var idEntryToSave = apiResponse.ToEntity(
            farmId,
            requestDto.Name,
            requestDto.FieldName,
            requestDto.ImageBase64Data
        );
            
        // 4. Add the IdEntry (with its suggestions) to your database
        try
        {
            var savedIdEntry = await idEntryRepository.AddIdEntryAsync(idEntryToSave);
            logger.LogInformation("Saved IdEntry with ID: {IdEntryId} to the database.", savedIdEntry.Id);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error saving IdEntry to the database for request: {Name}", requestDto.Name);
            // Decide how to handle database save failures (e.g., retry, log and return null, throw)
            return null;
        }
        
        // 5. Map the API response to your IdResponseDto and return it
        return apiResponse.ToDto();
    }

    public async ValueTask<IList<IdEntry>> GetAllIdEntriesByFarmIdAsync(Guid farmId)
    {
        // 1. Get all IdEntries for the given farm ID
        var idEntries = (await idEntryRepository.GetAllIdEntriesByFarmIdAsync(farmId)).ToList();
        
        // 2. Check if any IdEntries were found
        if (idEntries.Count == 0)
        {
            logger.LogWarning("No IdEntries found for Farm ID: {FarmId}", farmId);
            return new List<IdEntry>();
        }

        logger.LogInformation("Retrieved {Count} IdEntries for Farm ID: {FarmId}", idEntries.Count, farmId);
        return idEntries;
    }
}