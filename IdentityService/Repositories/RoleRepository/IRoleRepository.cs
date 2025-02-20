using IdentityService.Models;

namespace IdentityService.Repositories.RoleRepository;

public interface IRoleRepository
{
    ValueTask<Role?> GetRoleByNameAsync(string name);
}