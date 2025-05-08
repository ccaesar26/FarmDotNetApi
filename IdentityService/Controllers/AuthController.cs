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
        var result = await authService.LoginAsync(request.Email, request.Password);

        if (result.ErrorMessage != string.Empty)
        {
            return BadRequest(new { message = result.ErrorMessage });
        }

        tokenService.SetAuthTokenInCookie(result.AccessToken, HttpContext);
        tokenService.SetRefreshTokenInCookie(result.RefreshToken, HttpContext);

        return Ok(new { AccessToken = result.AccessToken, RefreshToken = result.RefreshToken });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        Response.Cookies.Delete("RefreshToken");
        return Ok(new { message = "Logged out" });
    }


    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto requestDto)
    {
        var refreshToken = requestDto.RefreshToken;
        if (string.IsNullOrEmpty(refreshToken))
        {
            // Fallback to cookie if needed for web (optional)
            refreshToken = Request.Cookies["RefreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Refresh token is missing");
            }
        }

        var refreshResult = await authService.RefreshAccessTokenAsync(refreshToken);

        if (!refreshResult.Success)
        {
            return Unauthorized(new { message = refreshResult.ErrorMessage });
        }

        tokenService.SetAuthTokenInCookie(refreshResult.AccessToken, HttpContext);
        tokenService.SetRefreshTokenInCookie(refreshResult.RefreshToken, HttpContext);

        return Ok(new
        {
            Message = "Token refreshed", 
            AccessToken = refreshResult.AccessToken,
            RefreshToken = refreshResult.RefreshToken
        });
    }
}

public record RefreshTokenRequestDto(string RefreshToken);