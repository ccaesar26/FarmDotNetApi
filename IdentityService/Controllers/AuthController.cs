using IdentityService.Models.Dtos;
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
}