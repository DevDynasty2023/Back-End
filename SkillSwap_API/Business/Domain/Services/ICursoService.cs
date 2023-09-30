using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Business.Domain.Services.Communication;

namespace SkillSwap_API.Business.Domain.Services;

public interface ICursoService
{
    Task<IEnumerable<Curso>> ListAsync();
    Task<Curso> GetByIdAsync(int id);
    Task<IEnumerable<Curso>> ListByUserIdAsync(int userId);
    Task<CursoResponse> SaveAsync(Curso store);
    Task<CursoResponse> UpdateAsync(int storeId, Curso store);
    Task<CursoResponse> DeleteAsync(int storeId);
}