using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.FarmAuthorizationService;
using Shared.FarmClaimTypes;
using Shared.Models.Events;
using UserProfileService.Extensions;
using UserProfileService.Models.Dtos;
using UserProfileService.Services;

namespace UserProfileService.Controllers;

[ApiController]
[Authorize(Policy = "ManagerAndWorkers")]
[Route("/api/user-profile")]
public class UserProfileController(
    IUserProfileService userProfileService,
    IFarmAuthorizationService farmAuthorizationService,
    IPublishEndpoint publishEndpoint
) : ControllerBase
{
    [HttpPost("create"), Authorize(Policy = "ManagerOnly")]
    public async Task<IActionResult> CreateUserProfileAsync([FromBody] CreateUserProfileRequest request)
    {
        var userId = farmAuthorizationService.GetUserId(); // This is the manager's user id
        if (!userId.HasValue)
        {
            return Unauthorized();
        }
        
        if (request.UserId is not null) // This means that a manager is creating a profile for a worker
        {
            userId = Guid.Parse(request.UserId);
        }

        var userProfileId = await userProfileService.AddUserProfileAsync(
            request.Name,
            request.DateOfBirth,
            request.Gender,
            userId.Value,
            request.Role
        );

        await publishEndpoint.Publish(new UserProfileCreatedEvent(userId.Value, userProfileId));

        return Ok(new { userProfileId });
    }

    [HttpGet]
    public async Task<IActionResult> GetUserProfileAsync()
    {
        var userId = farmAuthorizationService.GetUserId();
        if (!userId.HasValue)
        {
            return Unauthorized();
        }

        var userProfile = await userProfileService.GetUserProfileByUserIdAsync(userId.Value);
        if (userProfile is null)
        {
            return NotFound();
        }

        return Ok(userProfile.ToDto());
    }

    [HttpGet("user"), Authorize(Policy = "ManagerOnly")]
    public async Task<IActionResult> GetUserProfileByUserIdAsync([FromQuery] Guid userId)
    {
        var userProfile = await userProfileService.GetUserProfileByUserIdAsync(userId);

        if (userProfile is null)
        {
            return NotFound();
        }

        return Ok(userProfile.ToDto());
    }

    [HttpPut("{id:guid}"), Authorize(Policy = "ManagerOnly")]
    public async Task<IActionResult> UpdateUserProfileAsync(Guid id, [FromBody] UpdateUserProfileRequest request)
    {
        await userProfileService.UpdateUserProfileAsync(
            id,
            request.Name,
            request.DateOfBirth,
            request.Gender
        );

        return Ok();
    }

    [HttpDelete("{id:guid}"), Authorize(Policy = "ManagerOnly")]
    public async Task<IActionResult> DeleteUserProfileAsync(Guid id)
    {
        await userProfileService.DeleteUserProfileAsync(id);

        return Ok();
    }

    [HttpPost("attributes/assign"), Authorize(Policy = "ManagerOnly")]
    public async Task<IActionResult> AssignAttributeAsync([FromBody] AssignAttributesRequest request)
    {
        try
        {
            await userProfileService.AssignAttributesAsync(Guid.Parse(request.UserProfileId), request.AttributeNames);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpPost("attributes/remove/{userId}"), Authorize(Policy = "ManagerOnly")]
    public async Task<IActionResult> RemoveAttributeAsync(string userId, [FromBody] RemoveAttributesRequest request)
    {
        await userProfileService.RemoveAttributesAsync(Guid.Parse(userId), request.AttributeIds);

        return Ok();
    }
    
    [HttpGet("attributes")]
    public async Task<IActionResult> GetAttributesAsync()
    {
        var attributes = await userProfileService.GetAttributesAsync();
        
        var attributeMap = attributes.GroupBy(a => a.Category)
            .ToDictionary(g => g.Key.Name, g => g.Select(a => a.Name).ToList());
        
        return Ok(attributeMap);
    }
}