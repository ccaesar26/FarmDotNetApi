using ClimateIndicesService.ExternalClients.EdoApiClient;
using ClimateIndicesService.Models;
using ClimateIndicesService.Models.Dtos;
using ClimateIndicesService.Services;
using ClimateIndicesService.Services.DroughtAnalysisService;
using ClimateIndicesService.Services.DroughtRecordService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClimateIndicesService.Controllers;

[Authorize]
[ApiController]
[Route("api/drought-data")]
public class DroughtDataController(
    IDroughtAnalysisService droughtAnalysisService,
    IDroughtRecordService droughtRecordService,
    ILogger<DroughtDataController> logger
) : ControllerBase
{
    [HttpGet("drought-record")] // GET /DroughtData/droughtdata
    public async ValueTask<IActionResult> GetDroughtRecord()
    {
        try
        {
            var droughtData = await droughtRecordService.GetDroughtRecordAsync(DateTime.Now);

            var responseDto = new DroughtDataDto(
                droughtData.Date,
                Convert.ToBase64String(droughtData.RasterData)
            );

            return Ok(responseDto);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError($"Error in DroughtDataController (Edo API request): {ex.Message}");
            return StatusCode((int)System.Net.HttpStatusCode.InternalServerError,
                "Failed to fetch drought data from EDO API.");
        }
        catch (Exception ex)
        {
            logger.LogError($"Unexpected error in DroughtDataController (Drought Level Analysis): {ex}");
            return StatusCode((int)System.Net.HttpStatusCode.InternalServerError,
                "An unexpected error occurred while analyzing drought level.");
        }
    }
    
    [HttpGet("drought-level")]
    public async Task<IActionResult> GetDroughtLevelForPoint([FromQuery] double lon, [FromQuery] double lat,
        [FromQuery] DateTime? time)
    {
        try
        {
            if (time == DateTime.MinValue)
            {
                return BadRequest("Please provide a valid 'time' query parameter in yyyy-MM-dd format.");
            }

            var droughtRecord = await droughtRecordService.GetDroughtRecordAsync(time ?? DateTime.Now);

            var droughtLevelInfo =
                await droughtAnalysisService.ComputeDroughtLevelForCoordinates(droughtRecord.RasterData, lon, lat);

            return Ok(droughtLevelInfo);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError($"Error in DroughtDataController (Edo API request): {ex.Message}");
            return StatusCode((int)System.Net.HttpStatusCode.InternalServerError,
                "Failed to fetch drought data from EDO API.");
        }
        catch (Exception ex)
        {
            logger.LogError($"Unexpected error in DroughtDataController (Drought Level Analysis): {ex}");
            return StatusCode((int)System.Net.HttpStatusCode.InternalServerError,
                "An unexpected error occurred while analyzing drought level.");
        }
    }
}