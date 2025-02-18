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
    IAuthService authService
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
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true, // to prevent XSS
            SameSite = SameSiteMode.Strict, // to prevent CSRF
            Secure = true, // send only over HTTPS
            Expires = DateTime.UtcNow.AddHours(1)
        };
        
        Response.Cookies.Append("jwt", token, cookieOptions);

        return Ok(new { message = "Login successful" });
    }
    
    // [Authorize(Policy = "ManagerOnly")]
    // [HttpPost("upgrade-token")]
    // public async Task<IActionResult> UpgradeToken()
    // {
    //     var userId = farmAuthorizationService.GetUserId();
    //     if (userId == null)
    //     {
    //         return Unauthorized();
    //     }
    //     
    //     var user = await userService.GetUserAsync(userId.Value);
    //     if (user == null)
    //     {
    //         return Unauthorized();
    //     }
    //     
    //     var role = await authService.GetRoleAsync(userId.Value.ToString());
    //     
    //     var token = tokenService.GenerateJwtToken(user);
    //     
    //     return Ok(new { token, role });
    // }
}