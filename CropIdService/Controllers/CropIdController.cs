using CropIdService.Mapping.Extensions;
using CropIdService.Models.Dtos;
using CropIdService.Services.CropHealthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.FarmAuthorizationService;

namespace CropIdService.Controllers;

[ApiController]
[Authorize] // Require authentication for all actions in this controller
[Route("api/crop-id")]
public class CropIdController(
    ICropHealthService cropHealthService,
    IFarmAuthorizationService farmAuthorizationService,
    ILogger<CropIdController> logger
) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "ManagerOnly")]
    [ProducesResponseType(typeof(IdResponseDto[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllIdEntriesByFarmIdAsync()
    {
        // Get the farm ID from the authorization service
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId is null || farmId == Guid.Empty)
        {
            logger.LogError("Farm ID not found in claims");
            return Unauthorized("Farm ID not found in claims.");
        }

        // Call the service to get all ID entries
        var idEntries = (await cropHealthService.GetAllIdEntriesByFarmIdAsync(farmId.Value)).Select(e => e.ToDto());

        return Ok(idEntries);
    }
    
    [HttpPost("health")]
    [Authorize(Policy = "ManagerOnly")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(IdResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> IdentifyCropHealthAsync(
        [FromBody] IdRequestDto requestDto
    )
    {
        // Get the farm ID from the authorization service
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId is null || farmId == Guid.Empty)
        {
            logger.LogError("Farm ID not found in claims");
            return Unauthorized("Farm ID not found in claims.");
        }

        // Call the service to identify crop health
        var response = await cropHealthService.IdentifyCropHealthAsync(requestDto, farmId.Value);

        if (response != null)
        {
            return Ok(response);
        }
        
        logger.LogError("Failed to identify crop health for request: {Name}", requestDto.Name);
        return StatusCode(StatusCodes.Status500InternalServerError, "Failed to process the request.");

    }
}