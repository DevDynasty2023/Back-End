using Microsoft.EntityFrameworkCore;
using SkillSwap_API.Security.Domain.Models;
using SkillSwap_API.Security.Domain.Repositories;
using SkillSwap_API.Shared.Persistence.Contexts;
using SkillSwap_API.Shared.Persistence.Repositories;

namespace SkillSwap_API.Security.Persistence.Repositories;

public class RoleRepository : BaseRepository, IRoleRepository
{
public RoleRepository(AppDbContext context) : base(context)
{
}
    
public async Task<IEnumerable<Role>> ListAsync()
{
    return await _context.Roles.ToListAsync();
}

public async Task<Role> FindByIdAsync(int id)
{
    return await _context.Roles.FindAsync(id);
}

public async Task AddAsync(Role role)
{
    await _context.Roles.AddAsync(role);
}
}