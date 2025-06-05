using ClimateIndicesService.Models.Dtos;
using ClimateIndicesService.Services.NdviMapService;
using ClimateIndicesService.Services.NdviService;
using Microsoft.AspNetCore.Mvc;

namespace ClimateIndicesService.Controllers;

[ApiController]
[Route("api/climate-indices")]
public class ClimateDataController(
    INdviService ndviService,
    INdviMapService ndviMapService,
    ILogger<ClimateDataController> logger
) : ControllerBase
{
    [HttpGet("map-info/ndvi")]
    [ProducesResponseType(typeof(MapLayerInfo), StatusCodes.Status200OK)]
    public async Task<ActionResult<MapLayerInfo>> GetNdviMapInfo(CancellationToken cancellationToken)
    {
        try
        {
            var info = await ndviService.GetMapLayerInfoAsync(cancellationToken);
            return Ok(info);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting NDVI map info.");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error occurred while fetching map information.");
        }
    }

    [HttpGet("ndvi/timeseries")]
    [ProducesResponseType(typeof(IEnumerable<NdviDataPoint>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<NdviDataPoint>>> GetNdviTimeSeries(
        [FromQuery] double lat,
        [FromQuery] double lon,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        CancellationToken cancellationToken)
    {
        if (startDate.Kind == DateTimeKind.Unspecified) startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
        if (endDate.Kind == DateTimeKind.Unspecified) endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

        if (lat < -90 || lat > 90 || lon < -180 || lon > 180)
        {
            return BadRequest("Invalid latitude or longitude.");
        }

        if (startDate > endDate)
        {
            return BadRequest("Start date cannot be after end date.");
        }

        // Limit query range to avoid overloading the service or VITO's WMS
        if ((endDate - startDate).TotalDays > 90) // Example: Max 90 days
        {
            logger.LogWarning("NDVI time series request for {Days} days rejected (too long). Lat: {Lat}, Lon: {Lon}",
                (endDate - startDate).TotalDays, lat, lon);
            return BadRequest("Date range too large. Please query for a smaller period (e.g., up to 90 days).");
        }

        try
        {
            var data = await ndviService.GetNdviTimeSeriesAsync(lat, lon, startDate, endDate, cancellationToken);
            return Ok(data);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching NDVI time series for Lat: {Lat}, Lon: {Lon}", lat, lon);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error occurred while fetching NDVI time series data.");
        }
    }
    
    [HttpPost("ndvi-map/layer-info")] // Using POST because request includes a body (bbox, dates)
    public async Task<ActionResult<NdviMapLayerDetails>> GetNdviLayerInfoForMap([FromBody] NdviMapInfoRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (request.StartDate > request.EndDate)
        {
            return BadRequest("StartDate cannot be after EndDate.");
        }
        // Optional: Add a reasonable limit to the date range duration
        if ((request.EndDate - request.StartDate).TotalDays > 366 * 2) // e.g., 2 years
        {
            return BadRequest("Requested date range is too large. Please select a shorter period.");
        }


        try
        {
            var mapDetails = await ndviMapService.GetMapLayerDetailsAsync(request);
            return Ok(mapDetails);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving NDVI map layer details for request: {@Request}", request);
            return StatusCode(500, "An error occurred while fetching NDVI map layer details.");
        }
    }
}