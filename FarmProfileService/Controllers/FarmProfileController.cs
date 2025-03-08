using System.Security.Claims;
using FarmProfileService.Models;
using FarmProfileService.Models.Dtos;
using FarmProfileService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.FarmAuthorizationService;
using Shared.Models.Events;

namespace FarmProfileService.Controllers;

[ApiController]
[Authorize(policy: "ManagerOnly")]
[Route("/api/farm-profile")]
public class FarmProfileController(
    IFarmProfileService farmProfileService,
    IFarmAuthorizationService farmAuthorizationService,
    IPublishEndpoint publishEndpoint
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
        var userId = farmAuthorizationService.GetUserId();
        if (userId == null || userId.Value == Guid.Empty)
        {
            return Unauthorized();
        }
        
        var id = await farmProfileService.AddFarmProfileAsync(request.Name, request.Country, userId.Value);

        await publishEndpoint.Publish(new FarmCreatedEvent(userId.Value, id));

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFarmProfileAsync(Guid id)
    {
        var farmId = farmAuthorizationService.GetFarmId();
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
        var farmId = farmAuthorizationService.GetFarmId();
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