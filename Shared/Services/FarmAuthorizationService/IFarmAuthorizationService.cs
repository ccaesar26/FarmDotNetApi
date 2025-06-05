namespace Shared.FarmAuthorizationService;

public interface IFarmAuthorizationService
{
    Guid? GetFarmId();
    Guid? GetUserId();
    string GetUserRole();
}