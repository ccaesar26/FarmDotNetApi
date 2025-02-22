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
    IBus bus,
    IPublishEndpoint publishEndpoint
) : ControllerBase
{
    [HttpPost("debug-create")]
    public async Task<IActionResult> CreateUserProfileAsync()
    {
        var id = Guid.NewGuid();
        
        await publishEndpoint.Publish(new UserProfileCreatedEvent(id, id));
        
        return Ok();
    }

    [HttpPost("create"), Authorize(Policy = "ManagerOnly")]
    public async Task<IActionResult> CreateUserProfileAsync([FromBody] CreateUserProfileRequest request)
    {
        var userId = farmAuthorizationService.GetUserId();
        if (!userId.HasValue)
        {
            return Unauthorized();
        }
        
        var id = await userProfileService.AddUserProfileAsync(
            request.Name,
            request.DateOfBirth,
            request.Gender,
            userId.Value
        );
        
        await publishEndpoint.Publish(new UserProfileCreatedEvent(userId.Value, id));

        return Ok();
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
    
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetUserProfileByUserIdAsync(Guid userId)
    {
        var userProfile = await userProfileService.GetUserProfileByUserIdAsync(userId);

        if (userProfile is null)
        {
            return NotFound();
        }

        return Ok(userProfile);
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
}