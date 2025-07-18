﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Models;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.FarmClaimTypes;

namespace IdentityService.Services.TokenService;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string GenerateJwtToken(User user)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException());

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(FarmClaimTypes.UserId, user.Id.ToString()),
                new Claim(FarmClaimTypes.Email, user.Email),
                new Claim(FarmClaimTypes.Role, user.Role.Name),
                new Claim(FarmClaimTypes.FarmId, user.FarmId?.ToString() ?? string.Empty),
                new Claim(FarmClaimTypes.UserProfileId, user.UserProfileId?.ToString() ?? string.Empty)
            ]),
            Expires = DateTime.UtcNow.AddHours(double.Parse(jwtSettings["AccessTokenExpiryHours"] ?? "24")),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public FarmClaimTypes.FarmClaimDto? ValidateJwt(string jwt)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException());

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            var claimsPrincipal = tokenHandler.ValidateToken(jwt, tokenValidationParameters, out var securityToken);
            var userId = claimsPrincipal.FindFirst(FarmClaimTypes.UserId)?.Value;
            var email = claimsPrincipal.FindFirst(FarmClaimTypes.Email)?.Value;
            var role = claimsPrincipal.FindFirst(FarmClaimTypes.Role)?.Value;
            var farmId = claimsPrincipal.FindFirst(FarmClaimTypes.FarmId)?.Value;
            var userProfileId = claimsPrincipal.FindFirst(FarmClaimTypes.UserProfileId)?.Value;

            return new FarmClaimTypes.FarmClaimDto(
                userId ?? throw new InvalidOperationException(),
                email ?? throw new InvalidOperationException(),
                role ?? throw new InvalidOperationException(),
                farmId ?? throw new InvalidOperationException(),
                userProfileId ?? throw new InvalidOperationException()
            );
        }
        catch
        {
            return null;
        }
    }

    public void SetAuthTokenInCookie(string token, HttpContext context)
    {
        context.Response.Cookies.Append("AuthToken", token, new CookieOptions
        {
            Expires = DateTime.UtcNow.AddSeconds(double.Parse(configuration["Jwt:AccessTokenExpirySeconds"] ?? "60")),  
            HttpOnly = true,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
    }

    public void SetRefreshTokenInCookie(string token, HttpContext context)
    {
        context.Response.Cookies.Append("RefreshToken", token, new CookieOptions
        {
            Expires = DateTime.UtcNow.AddDays(double.Parse(configuration["Jwt:RefreshTokenExpiryDays"] ?? "30")),
            HttpOnly = true,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
    }
}