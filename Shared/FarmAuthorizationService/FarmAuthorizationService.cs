namespace Shared.FarmAuthorizationService;

public class FarmAuthorizationService(IHttpContextAccessor httpContextAccessor) : IFarmAuthorizationService
{
    public Guid? GetUserFarmId()
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
}