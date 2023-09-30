using SkillSwap_API.Security.Domain.Models;

namespace SkillSwap_API.Security.Domain.Repositories;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> ListAsync();
    Task<Role> FindByIdAsync(int id);
    Task AddAsync(Role role);
}