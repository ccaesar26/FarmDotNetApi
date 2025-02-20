using IdentityService.Models;

namespace IdentityService.Services.RoleService;

public interface IRoleService
{
    ValueTask<Role?> GetRoleByNameAsync(string name);
}