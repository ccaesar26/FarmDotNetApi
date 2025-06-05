using CropIdService.Models.Dtos;
using CropIdService.Models.Entities;

namespace CropIdService.Services.CropHealthService;

public interface ICropHealthService
{
    /// <summary>
    /// Processes an identification request, calls the external API, saves the result,
    /// and returns a response DTO.
    /// </summary>
    /// <param name="requestDto">The identification request data.</param>
    /// <param name="farmId"></param>
    /// <returns>An IdResponseDto containing the processed information, or null if an error occurs.</returns>
    ValueTask<IdResponseDto?> IdentifyCropHealthAsync(IdRequestDto requestDto, Guid farmId);
    
    ValueTask<IList<IdEntry>> GetAllIdEntriesByFarmIdAsync(Guid farmId);
}