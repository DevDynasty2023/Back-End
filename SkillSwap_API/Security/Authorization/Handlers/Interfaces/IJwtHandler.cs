using SkillSwap_API.Security.Domain.Models;

namespace SkillSwap_API.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    public string GenerateToken(User user);
    public int? ValidateToken(string token);
}