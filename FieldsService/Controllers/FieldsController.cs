using FieldsService.Models.Dtos;
using FieldsService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.FarmAuthorizationService;

namespace FieldsService.Controllers;

[Authorize(Policy = "WorkerOrManager")]
[ApiController]
[Route("/api/[controller]")]
public class FieldsController(
    IFieldsService fieldsService,
    IFarmAuthorizationService farmAuthorizationService
) : ControllerBase
{
    [HttpGet("debug-manager-only"), Authorize(Policy = "ManagerOnly")]
    public IActionResult DebugManagerOnly()
    {
        var farmId = farmAuthorizationService.GetUserFarmId();
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
        var farmId = farmAuthorizationService.GetUserFarmId();
        return Ok(new { farmId });
    }

    [Authorize(Policy = "ManagerOnly")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateFieldAsync([FromBody] CreateFieldRequest request)
    {
        var farmId = farmAuthorizationService.GetUserFarmId();
        if (request.FarmId != farmId)
        {
            return Forbid("You are not authorized to create fields for this farm.");
        }

        try
        {
            var field = await fieldsService.AddFieldAsync(request.FarmId, request.FieldName, request.FieldBoundary);
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

        var userFarmId = farmAuthorizationService.GetUserFarmId();
        if (field.FarmId != userFarmId)
        {
            return Forbid("You are not authorized to view this field.");
        }

        return Ok(field);
    }

    [HttpGet("farm/{farmId:guid}")]
    public async Task<IActionResult> GetFieldsAsync(Guid farmId)
    {
        var userFarmId = farmAuthorizationService.GetUserFarmId();
        if (farmId != userFarmId)
        {
            return Forbid("You are not authorized to view fields for this farm.");
        }

        var fields = await fieldsService.GetFieldsByFarmIdAsync(farmId);
        return Ok(fields);
    }

    [Authorize(Policy = "ManagerOnly")]
    [HttpPut("{fieldId:guid}")]
    public async Task<IActionResult> UpdateFieldAsync(Guid fieldId, [FromBody] UpdateFieldRequest request)
    {
        var farmId = farmAuthorizationService.GetUserFarmId();
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
        var farmId = farmAuthorizationService.GetUserFarmId();
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
        var userFarmId = farmAuthorizationService.GetUserFarmId();
        if (farmId != userFarmId)
        {
            return Forbid("You are not authorized to check fields for this farm.");
        }

        var exists = await fieldsService.FieldExistsByNameAsync(farmId, fieldName);
        return Ok(new { exists });
    }
}