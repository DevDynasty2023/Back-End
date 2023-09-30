using SkillSwap_API.Security.Domain.Models;
using SkillSwap_API.Security.Domain.Services.Communication;

namespace SkillSwap_API.Security.Domain.Services;

public interface IRoleService
{
    Task<IEnumerable<Role>> ListAsync();
    Task<RoleResponse> SaveAsync(Role role);
}