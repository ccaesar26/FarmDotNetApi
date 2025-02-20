using IdentityService.Models.Dtos;
using IdentityService.Services.AuthService;
using IdentityService.Services.TokenService;
using IdentityService.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController(
    IAuthService authService,
    ITokenService tokenService,
    IUserService userService
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

        tokenService.SetTokenInCookie(token, HttpContext);

        return Ok(new { message = "Login successful" });
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        return Ok(new { message = "Logged out" });
    }

    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var jwt = Request.Cookies["AuthToken"];
        if (string.IsNullOrEmpty(jwt))
        {
            return Unauthorized();
        }

        var userClaim = tokenService.ValidateJwt(jwt);
        if (userClaim == null)
        {
            return Unauthorized();
        }

        var user = await userService.GetUserAsync(new Guid(userClaim.UserId));

        var newToken = tokenService.GenerateJwtToken(user ?? throw new InvalidOperationException());

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.None,
            IsEssential = true,
            Expires = DateTime.UtcNow.AddHours(1)
        };

        Response.Cookies.Append("jwt", newToken, cookieOptions);

        return Ok(new { message = "Token refreshed" });
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