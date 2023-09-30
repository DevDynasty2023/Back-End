using SkillSwap_API.Security.Domain.Models;
using SkillSwap_API.Shared.Domain.Services.Communication;

namespace SkillSwap_API.Security.Domain.Services.Communication;

public class RoleResponse : BaseResponse<Role>
{
    public RoleResponse(string message) : base(message)
    {
    }

    public RoleResponse(Role model) : base(model)
    {
    }
}