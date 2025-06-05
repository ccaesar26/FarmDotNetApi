namespace Shared.FarmAuthorizationService;

public class FarmAuthorizationService(IHttpContextAccessor httpContextAccessor) : IFarmAuthorizationService
{
    public Guid? GetFarmId()
    {
        var claim = httpContextAccessor.HttpContext?.User.FindFirst("farmId");
        if (claim == null)
        {
            throw new UnauthorizedAccessException("Farm ID is missing from token.");
        }
        if (claim.Value == string.Empty)
        {
            return null;
        }
        return Guid.Parse(claim.Value);
    }
    
    public Guid? GetUserId()
    {
        var claim = httpContextAccessor.HttpContext?.User.FindFirst("userId");
        if (claim == null)
        {
            throw new UnauthorizedAccessException("User ID is missing from token.");
        }
        if (claim.Value == string.Empty)
        {
            return null;
        }
        return Guid.Parse(claim.Value);
    }

    public string GetUserRole()
    {
        var claim = httpContextAccessor.HttpContext?.User.FindFirst("role");
        if (claim == null)
        {
            throw new UnauthorizedAccessException("Role is missing from token.");
        }
        
        return claim.Value;
    }
}