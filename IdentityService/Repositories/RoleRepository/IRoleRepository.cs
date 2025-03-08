using IdentityService.Models;
using IdentityService.Models.Entities;

namespace IdentityService.Repositories.RoleRepository;

public interface IRoleRepository
{
    ValueTask<Role?> GetRoleByNameAsync(string name);
}