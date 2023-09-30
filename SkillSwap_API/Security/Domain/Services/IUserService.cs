using SkillSwap_API.Security.Domain.Models;
using SkillSwap_API.Security.Domain.Services.Communication;

namespace SkillSwap_API.Security.Domain.Services;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest resource);
    Task<IEnumerable<User>> ListAsync();
    Task<User> GetByIdAsync(int id);
    Task<User> GetByIdAsyncV2(int id);
    Task RegisterAsync(RegisterRequest resource);
    Task UpdateAsync(int id, UpdateRequest resource);
    Task DeleteAsync(int id);
}