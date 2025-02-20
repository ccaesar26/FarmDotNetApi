using IdentityService.Models;
using IdentityService.Repositories.RoleRepository;

namespace IdentityService.Services.RoleService;

public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    public async ValueTask<Role?> GetRoleByNameAsync(string name)
    {
        return await roleRepository.GetRoleByNameAsync(name);
    }
}