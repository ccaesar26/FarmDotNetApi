using Microsoft.AspNetCore.Mvc;
using PlantedCropsService.Models.Dtos;
using PlantedCropsService.Services.CropService;
using Shared.FarmAuthorizationService;

namespace PlantedCropsService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CropsController(
    ICropService cropService,
    IFarmAuthorizationService farmAuthorizationService
) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetCropsAsync()
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId is null)
        {
            return Unauthorized();
        }
        
        var crops = await cropService.GetAllCropsAsync(farmId.Value);
        return Ok(crops);
    }
    
    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> GetCropByIdAsync(Guid id)
    {
        var crop = await cropService.GetCropByIdAsync(id);
        if (crop is null)
        {
            return NotFound();
        }
        return Ok(crop);
    }
    
    [HttpPost]
    public async ValueTask<IActionResult> CreateCropAsync([FromBody] CropCreateDto dto)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId is null)
        {
            return Unauthorized();
        }
        
        var crop = await cropService.AddCropAsync(dto, farmId.Value);
        return CreatedAtAction("GetCropById", new { id = crop.Id }, crop);
    }
    
    [HttpPut("{id:guid}")]
    public async ValueTask<IActionResult> UpdateCropAsync(Guid id, [FromBody] CropUpdateDto updateDto)
    {
        if (id != updateDto.Id) // Ensure ID in route matches ID in DTO
        {
            return BadRequest("ID in route and request body must match.");
        }

        try
        {
            await cropService.UpdateCropAsync(updateDto);
            return NoContent(); // 204 No Content - successful update
        }
        catch (KeyNotFoundException)
        {
            return NotFound(); // 404 Not Found if crop with given ID doesn't exist
        }
        catch (Exception) // Catch other potential exceptions (consider more specific exception handling)
        {
            return StatusCode(500, "Internal Server Error"); // 500 Internal Server Error
        }
    }
    
    [HttpDelete("{id:guid}")]
    public async ValueTask<IActionResult> DeleteCropAsync(Guid id)
    {
        try
        {
            await cropService.DeleteCropAsync(id);
            return NoContent(); // 204 No Content - successful deletion
        }
        catch (KeyNotFoundException)
        {
            return NotFound(); // 404 Not Found if crop with given ID doesn't exist
        }
        catch (Exception) // Catch other potential exceptions (consider more specific exception handling)
        {
            return StatusCode(500, "Internal Server Error"); // 500 Internal Server Error
        }
    }
}