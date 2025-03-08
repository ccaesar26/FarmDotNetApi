using UserProfileService.Models.Entities;

namespace UserProfileService.Repositories.AttributeCategoryRepository;

public interface IAttributeCategoryRepository
{
    ValueTask<AttributeCategory?> GetByNameAsync(string name);
}