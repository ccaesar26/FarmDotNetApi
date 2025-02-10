using System.Security.Claims;
using FarmProfileService.Models;
using FarmProfileService.Models.Dtos;
using FarmProfileService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.FarmAuthorizationService;

namespace FarmProfileService.Controllers;

[ApiController]
[Authorize(policy: "ManagerOnly")]
[Route("/api/farm-profile")]
public class FarmProfileController(
    IFarmProfileService farmProfileService,
    IFarmAuthorizationService farmAuthorizationService
) : ControllerBase
{
    [HttpGet("debug")]
    public ActionResult<string> DebugClaims()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        return Ok(new { claims, role });
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateFarmProfileAsync([FromBody] CreateFarmProfileRequest request)
    {
        await farmProfileService.AddFarmProfileAsync(request.Name, request.Country, request.OwnerId);

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFarmProfileAsync(Guid id)
    {
        var farmId = farmAuthorizationService.GetUserFarmId();
        if (id != farmId)
        {
            return Forbid("You are not authorized to create fields for this farm.");
        }

        var farmProfile = await farmProfileService.GetFarmProfileAsync(id);

        if (farmProfile is null)
        {
            return NotFound();
        }

        return Ok(farmProfile);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateFarmProfileAsync(Guid id, [FromBody] UpdateFarmProfileRequest request)
    {
        var farmId = farmAuthorizationService.GetUserFarmId();
        if (id != farmId)
        {
            return Forbid("You are not authorized to create fields for this farm.");
        }
        
        await farmProfileService.UpdateFarmProfileAsync(
            id,
            request.Name,
            request.Country
        );

        return Ok();
    }
}