using IdentityService.Models.Dtos;
using IdentityService.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.FarmAuthorizationService;
using Shared.FarmClaimTypes;

namespace IdentityService.Controllers;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class UsersController(
    IUserService userService,
    IFarmAuthorizationService farmAuthorizationService
) : Controller
{
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
    public IActionResult GetUserRole()
    {
        var roleClaim = User.FindFirst(FarmClaimTypes.Role);
        
        if (roleClaim == null)
        {
            return Unauthorized(new { message = "User role not found" });
        }
        
        return Ok(new { role = roleClaim.Value });
    }

}