namespace Shared.FarmAuthorizationService;

public interface IFarmAuthorizationService
{
    Guid? GetUserFarmId();

    public Guid? GetUserId();
}