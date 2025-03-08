using UserProfileService.Models.Entities;

namespace UserProfileService.Repositories.ProfileAttributeRepository;

public interface IProfileAttributeRepository
{
    ValueTask<ProfileAttribute?> GetByIdAsync(Guid id);
    
    ValueTask<ProfileAttribute?> GetByNameAsync(string name);
    
    ValueTask<ProfileAttribute> AddAsync(ProfileAttribute profileAttribute);
    ValueTask<ProfileAttribute?> GetByNameAndCategoryNameAsync(string name, string category);
    
    ValueTask<IEnumerable<ProfileAttribute>> GetAllAsync();

    ValueTask AddAttribute(ProfileAttribute profileAttribute);
}