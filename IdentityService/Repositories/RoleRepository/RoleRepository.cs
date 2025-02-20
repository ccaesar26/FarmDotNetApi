using IdentityService.Data;
using IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Repositories.RoleRepository;

public class RoleRepository(IdentityDbContext context) : IRoleRepository
{
    public async ValueTask<Role?> GetRoleByNameAsync(string name)
    {
        return await context.Roles.SingleOrDefaultAsync(r => r.Name == name);
    }
}