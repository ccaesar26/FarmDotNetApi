namespace Shared.FarmAuthorizationService;

public interface IFarmAuthorizationService
{
    Guid? GetFarmId();

    public Guid? GetUserId();
}