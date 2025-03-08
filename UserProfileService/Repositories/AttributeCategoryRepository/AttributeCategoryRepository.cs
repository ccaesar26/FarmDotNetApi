using Microsoft.EntityFrameworkCore;
using UserProfileService.Data;
using UserProfileService.Models.Entities;

namespace UserProfileService.Repositories.AttributeCategoryRepository;

public class AttributeCategoryRepository(UserProfileDbContext context) : IAttributeCategoryRepository
{
    public async ValueTask<AttributeCategory?> GetByNameAsync(string name)
    {
        return await context.AttributeCategories
            .FirstOrDefaultAsync(ac => ac != null && ac.Name == name);
    }
}