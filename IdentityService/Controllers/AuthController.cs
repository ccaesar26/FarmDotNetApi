using IdentityService.Models.Dtos;
using IdentityService.Services.AuthService;
using IdentityService.Services.TokenService;
using IdentityService.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.FarmAuthorizationService;

namespace IdentityService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController(
    IUserService userService,
    IAuthService authService,
    ITokenService tokenService,
    IFarmAuthorizationService farmAuthorizationService
) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        try
        {
            await authService.RegisterAsync(
                request.Username,
                request.Email,
                request.Password,
                request.Role,
                request.FarmId
            );
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await authService.AuthenticateAsync(request.Email, request.Password);

        if (token == null)
        {
            return Unauthorized("Invalid credentials");
        }

        var role = await authService.GetRoleAsync(request.Email);

        return Ok(new { token, role });
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
        
        await authService.UpdateFarmIdAsync(userId.ToString()!, request.FarmId);
        
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
        
        await authService.UpdateUserProfileAsync(userId.ToString()!, request.UserProfileId);
        
        return Ok();
    }
    
    [Authorize(Policy = "ManagerOnly")]
    [HttpPost("upgrade-token")]
    public async Task<IActionResult> UpgradeToken()
    {
        var userId = farmAuthorizationService.GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }
        
        var user = await userService.GetUserAsync(userId.Value);
        if (user == null)
        {
            return Unauthorized();
        }
        
        var role = await authService.GetRoleAsync(userId.Value.ToString());
        
        var token = tokenService.GenerateJwtToken(user);
        
        return Ok(new { token, role });
    }
}