using FarmProfileService.Data;
using FarmProfileService.Models.Entities;

namespace FarmProfileService.Repositories;

public class FarmProfileRepository(FarmProfileDbContext context) : IFarmProfileRepository
{
    public async ValueTask<FarmProfile?> GetByIdAsync(Guid id)
    {
        return await context.FarmProfiles.FindAsync(id);
    }

    public async ValueTask<FarmProfile> AddAsync(FarmProfile farmProfile)
    {
        farmProfile = (await context.FarmProfiles.AddAsync(farmProfile)).Entity;
        await context.SaveChangesAsync();
        return farmProfile;
    }

    public async ValueTask UpdateAsync(FarmProfile farmProfile)
    {
        context.FarmProfiles.Update(farmProfile);
        await context.SaveChangesAsync();
    }

    public async ValueTask DeleteAsync(Guid id)
    {
        var farmProfile = await context.FarmProfiles.FindAsync(id);
        if (farmProfile is not null)
        {
            context.FarmProfiles.Remove(farmProfile);
            await context.SaveChangesAsync();
        }
    }
}