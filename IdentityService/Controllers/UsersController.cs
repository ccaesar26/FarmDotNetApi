using IdentityService.Extensions;
using IdentityService.Models.Dtos;
using IdentityService.Services.UserService;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.FarmAuthorizationService;
using Shared.FarmClaimTypes;
using Shared.Models.Events;

namespace IdentityService.Controllers;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class UsersController(
    IUserService userService,
    IFarmAuthorizationService farmAuthorizationService,
    IPublishEndpoint publishEndpoint
) : Controller
{
    [Authorize(Policy = "ManagerOnly")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null || string.IsNullOrEmpty(farmId.ToString()))
        {
            return Unauthorized();
        }
        
        try
        {
            var userId = await userService.CreateUserAsync(
                request.Username,
                request.Email,
                request.Password,
                request.Role,
                farmId.ToString()
            );
            return Ok(new { userId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [Authorize(Policy = "ManagerOnly")]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request)
    {
        var userId = farmAuthorizationService.GetUserId();
        if (farmAuthorizationService.GetUserId() == null || string.IsNullOrEmpty(userId.ToString()))
        {
            return Unauthorized();
        }

        try
        {
            var updatedUser = await userService.UpdateUserAsync(request);
            return Ok(updatedUser.ToUpdateUserResponse());
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [Authorize(Policy = "ManagerOnly")]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUserAsync(Guid userId)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null || string.IsNullOrEmpty(farmId.ToString()))
        {
            return Unauthorized();
        }

        try
        {
            await userService.DeleteUserAsync(userId);
            await publishEndpoint.Publish(new UserDeletedEvent(userId));
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [Authorize(Policy = "ManagerOnly")]
    [HttpPut("update-farm-id")]
    public async Task<IActionResult> UpdateFarmIdAsync([FromBody] UpdateFarmRequest request)
    {
        var userId = farmAuthorizationService.GetUserId();
        if (userId == null || string.IsNullOrEmpty(userId.ToString()))
        {
            return Unauthorized();
        }

        await userService.UpdateFarmIdAsync(userId.ToString()!, request.FarmId);

        return Ok();
    }

    [Authorize(Policy = "ManagerOnly")]
    [HttpPut("update-user-profile-id")]
    public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UpdateUserProfileRequest request)
    {
        var userId = farmAuthorizationService.GetUserId();
        if (userId == null || string.IsNullOrEmpty(userId.ToString()))
        {
            return Unauthorized();
        }

        await userService.UpdateUserProfileAsync(userId.ToString()!, request.UserProfileId);

        return Ok();
    }
    
    [HttpGet("me")]
    public async ValueTask<IActionResult> GetUserRole()
    {
        var userId = farmAuthorizationService.GetUserId();
        if (userId == null || string.IsNullOrEmpty(userId.ToString()))
        {
            return Unauthorized();
        }
        
        var roleClaim = User.FindFirst(FarmClaimTypes.Role);
        if (roleClaim == null)
        {
            return Unauthorized(new { message = "User role not found" });
        }
        
        var user = await userService.GetUserAsync(userId.Value);
        if (user is null)
        {
            return Unauthorized(new { message = "User not found" });
        }
        
        if (user.Role.Name != roleClaim.Value)
        {
            return Unauthorized();
        }
        
        return Ok(new { username = user.Username, email = user.Email, role = roleClaim.Value });
    }
    
    [HttpGet("workers")]
    public async Task<IActionResult> GetWorkersAsync()
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null || string.IsNullOrEmpty(farmId.ToString()))
        {
            return Unauthorized();
        }

        var workers = await userService.GetWorkersAsync(farmId);
        return Ok(workers.ToArray());
    }
}