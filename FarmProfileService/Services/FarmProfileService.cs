using FarmProfileService.Models.Entities;
using FarmProfileService.Repositories;

namespace FarmProfileService.Services;

public class FarmProfileService(IFarmProfileRepository farmProfileRepository) : IFarmProfileService
{
    public async ValueTask<FarmProfile?> GetFarmProfileAsync(Guid id)
    {
        return await farmProfileRepository.GetByIdAsync(id);
    }

    public async ValueTask AddFarmProfileAsync(string name, string country, Guid ownerId)
    {
        var farmProfile = new FarmProfile
        {
            Name = name,
            Country = country,
            OwnerId = ownerId
        };
        
        await farmProfileRepository.AddAsync(farmProfile);
    }

    public async ValueTask UpdateFarmProfileAsync(Guid id, string name, string country)
    {
        var farmProfile = new FarmProfile
        {
            Id = id,
            Name = name,
            Country = country
        };
        
        await farmProfileRepository.UpdateAsync(farmProfile);
    }

    public async ValueTask DeleteFarmProfileAsync(Guid id)
    {
        await farmProfileRepository.DeleteAsync(id);
    }
}