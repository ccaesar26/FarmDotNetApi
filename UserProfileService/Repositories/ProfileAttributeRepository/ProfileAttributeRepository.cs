using Microsoft.EntityFrameworkCore;
using UserProfileService.Data;
using UserProfileService.Models.Entities;

namespace UserProfileService.Repositories.ProfileAttributeRepository;

public class ProfileAttributeRepository(UserProfileDbContext context) : IProfileAttributeRepository
{
    public async ValueTask<ProfileAttribute?> GetByIdAsync(Guid id)
    {
        return await context.ProfileAttributes.FindAsync(id);
    }

    public async ValueTask<ProfileAttribute?> GetByNameAsync(string name)
    {
        return await context.ProfileAttributes
            .Include(pa => pa.Category)
            .FirstOrDefaultAsync(pa => pa.Name == name);
    }

    public async ValueTask<ProfileAttribute> AddAsync(ProfileAttribute profileAttribute)
    {
        var ret = await context.ProfileAttributes.AddAsync(profileAttribute);
        await context.SaveChangesAsync();
        return ret.Entity;
    }

    public async ValueTask<ProfileAttribute?> GetByNameAndCategoryNameAsync(string name, string category)
    {
        return await context.ProfileAttributes
            .Include(pa => pa.Category)
            .FirstOrDefaultAsync(pa => pa.Name == name && pa.Category.Name == category);
    }

    public async ValueTask<IEnumerable<ProfileAttribute>> GetAllAsync()
    {
        return await context.ProfileAttributes
            .Include(pa => pa.Category)
            .ToListAsync();
    }
    
    public async ValueTask AddAttribute(ProfileAttribute profileAttribute)
    {
        await context.ProfileAttributes.AddAsync(profileAttribute);
        await context.SaveChangesAsync();
    }
}