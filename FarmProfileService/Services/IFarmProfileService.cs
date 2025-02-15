using FarmProfileService.Models.Entities;

namespace FarmProfileService.Services;

public interface IFarmProfileService
{
    public ValueTask<FarmProfile?> GetFarmProfileAsync(Guid id);

    public ValueTask<Guid> AddFarmProfileAsync(string name, string country, Guid ownerId);

    public ValueTask UpdateFarmProfileAsync(Guid id, string name, string country);

    public ValueTask DeleteFarmProfileAsync(Guid id);
}