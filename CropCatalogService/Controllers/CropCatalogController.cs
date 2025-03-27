using CropCatalogService.Model.Dtos;
using CropCatalogService.Model.Entities;
using CropCatalogService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CropCatalogService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CropCatalogController(
    ICropCatalogService cropCatalogService
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CropCatalogDto>>> GetCrops()
    {
        var crops = await cropCatalogService.GetAllCropsAsync();
        return Ok(crops);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CropCatalogDto>> GetCropById(Guid id)
    {
        var crop = await cropCatalogService.GetCropByIdAsync(id);
        if (crop == null)
        {
            return NotFound();
        }
        return Ok(crop);
    }
    
    [HttpPost]
    public async Task<ActionResult<CropCatalogDto>> AddCrop([FromBody] CreateCropCatalogDto crop)
    {
        var c = await cropCatalogService.AddCropAsync(crop);
        return CreatedAtAction(nameof(GetCropById), new { id = c.Id }, crop);
    }
    
    [HttpPut("{id}"), Authorize(Policy = "ManagerOnly")]
    public async Task<IActionResult> UpdateCrop(Guid id, [FromBody] UpdateCropCatalogDto crop)
    {
        if (id != crop.Id)
        {
            return BadRequest("Crop ID mismatch");
        }
        
        if (!await cropCatalogService.CropExistsAsync(id))
        {
            return NotFound();
        }
        
        await cropCatalogService.UpdateCropAsync(crop);
        return NoContent();
    }
    
    [HttpDelete("{id}"), Authorize(Policy = "ManagerOnly")]
    public async Task<IActionResult> DeleteCrop(Guid id)
    {
        if (!await cropCatalogService.CropExistsAsync(id))
        {
            return NotFound();
        }
        
        await cropCatalogService.DeleteCropAsync(id);
        return NoContent();
    }
}