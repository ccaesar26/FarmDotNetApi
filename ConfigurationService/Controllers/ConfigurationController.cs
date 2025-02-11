using ConfigurationService.Data;
using ConfigurationService.Models;
using ConfigurationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationService.Controllers;

[ApiController]
[Authorize]
[Route("api/config")]
public class ConfigurationController(
    ConfigDbContext dbContext,
    EncryptionService encryptionService
) : ControllerBase
{
    [HttpPost("store"), Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> StoreConfig([FromBody] ConfigRequest request)
    {
        var encryptedValue = encryptionService.Encrypt(request.Value);
        var setting = new ConfigSetting { Key = request.Key, EncryptedValue = encryptedValue };

        dbContext.ConfigSettings.Add(setting);
        await dbContext.SaveChangesAsync();

        return Ok(new { message = "Configuration stored securely." });
    }

    [HttpGet("retrieve/{key}")]
    public async Task<IActionResult> RetrieveConfig(string key)
    {
        var setting = await dbContext.ConfigSettings.FirstOrDefaultAsync(s => s.Key == key);
        if (setting == null)
            return NotFound("Configuration key not found.");

        var decryptedValue = encryptionService.Decrypt(setting.EncryptedValue);
        return Ok(new { key = setting.Key, value = decryptedValue });
    }
}

public class ConfigRequest
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}