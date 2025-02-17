using System.Security.Claims;

namespace Shared.FarmClaimTypes;

public static class FarmClaimTypes
{
    public const string UserId = "userId";
    public const string Email = ClaimTypes.Email;
    public const string Role = ClaimTypes.Role;
    public const string FarmId = "farmId";
    public const string UserProfileId = "userProfileId";
}