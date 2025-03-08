using FieldsService.Models.Dtos;
using FieldsService.Services;
using FieldsService.Services.GeocodingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.FarmAuthorizationService;

namespace FieldsService.Controllers;

[Authorize(Policy = "WorkerOrManager")]
[ApiController]
[Route("/api/[controller]")]
public class FieldsController(
    IFieldsService fieldsService,
    IGeocodingService geocodingService,
    IFarmAuthorizationService farmAuthorizationService
) : ControllerBase
{
    [HttpGet("debug-manager-only"), Authorize(Policy = "ManagerOnly")]
    public IActionResult DebugManagerOnly()
    {
        var farmId = farmAuthorizationService.GetFarmId();
        return Ok(new { farmId });
    }

    [HttpGet("debug-worker-or-manager")]
    public IActionResult DebugWorkerOrManager()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value });
        return Ok(new { claims });
    }
    
    [Authorize(Policy = "ManagerOnly")]
    [HttpGet("has-farm")]
    public IActionResult HasFarm()
    {
        var farmId = farmAuthorizationService.GetFarmId();
        return Ok(new { farmId });
    }

    [Authorize(Policy = "ManagerOnly")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateFieldAsync([FromBody] CreateFieldRequest request)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId is null)
        {
            return Forbid("You are not authorized to create fields for this farm.");
        }

        try
        {
            var field = await fieldsService.AddFieldAsync(farmId.Value, request.FieldName, request.FieldBoundary);
            return Ok(new { field });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{fieldId:guid}")]
    public async Task<IActionResult> GetFieldAsync(Guid fieldId)
    {
        var field = await fieldsService.GetFieldByIdAsync(fieldId);
        if (field == null)
        {
            return NotFound();
        }

        var userFarmId = farmAuthorizationService.GetFarmId();
        if (field.FarmId != userFarmId)
        {
            return Forbid("You are not authorized to view this field.");
        }

        return Ok(field);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetFieldsAsync()
    {
        var userFarmId = farmAuthorizationService.GetFarmId();
        if (userFarmId is null)
        {
            return Forbid("You are not authorized to view fields for this farm.");
        }

        var fields = await fieldsService.GetFieldsByFarmIdAsync(userFarmId.Value);
        return Ok(fields);
    }

    [Authorize(Policy = "ManagerOnly")]
    [HttpPut("{fieldId:guid}")]
    public async Task<IActionResult> UpdateFieldAsync(Guid fieldId, [FromBody] UpdateFieldRequest request)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (request.FarmId != farmId)
        {
            return Forbid("You are not authorized to update fields for this farm.");
        }

        try
        {
            await fieldsService.UpdateFieldAsync(fieldId, request.FarmId, request.FieldName, request.FieldBoundary);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Policy = "ManagerOnly")]
    [HttpDelete("{fieldId:guid}")]
    public async Task<IActionResult> DeleteFieldAsync(Guid fieldId)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        var field = await fieldsService.GetFieldByIdAsync(fieldId);
        if (field != null && farmId != field.FarmId)
        {
            return Forbid("You are not authorized to delete fields for this farm.");
        }

        try
        {
            await fieldsService.DeleteFieldAsync(fieldId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("exists/{farmId:guid}/{fieldName}")]
    public async Task<IActionResult> FieldNameExistsAsync(Guid farmId, string fieldName)
    {
        var userFarmId = farmAuthorizationService.GetFarmId();
        if (farmId != userFarmId)
        {
            return Forbid("You are not authorized to check fields for this farm.");
        }

        var exists = await fieldsService.FieldExistsByNameAsync(farmId, fieldName);
        return Ok(new { exists });
    }
    
    [HttpGet("coordinates")]
    public async Task<IActionResult> GetFieldCoordinatesAsync()
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId is null)
        {
            return Forbid("You are not authorized to view fields for this farm.");
        }

        var coordinates = await fieldsService.GetFieldsCoordinatesAsync(farmId.Value);
        return Ok(new { coordinates });
    }
    
    [HttpGet("cities")]
    public async Task<IActionResult> GetFieldsCitiesAsync()
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId is null)
        {
            return Forbid("You are not authorized to view fields for this farm.");
        }

        var coordinates = await fieldsService.GetFieldsCoordinatesAsync(farmId.Value);
        
        var cities = new HashSet<GeocodingResult>();
        foreach (var coordinate in coordinates)
        {
            var city = await geocodingService.GetCityByCoordinatesAsync(coordinate.Y, coordinate.X);
            if (city != null)
            {
                cities.Add(city);
            }
        }
        
        return Ok(cities.ToList());
    }
}