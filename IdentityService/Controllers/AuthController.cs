using IdentityService.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        try
        {
            await authService.RegisterAsync(request.Username, request.Email, request.Password, null);
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

        return Ok(new { token });
    }

    public record RegisterRequest(string Username, string Email, string Password, string Role);

    public record LoginRequest(string Email, string Password);
}