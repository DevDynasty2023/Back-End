using SkillSwap_API.Business.Domain.Models;

namespace SkillSwap_API.Business.Domain.Repositories;

public interface ICursoRepository
{
    Task<IEnumerable<Curso>> ListAsync();
    Task AddAsync(Curso store);
    Task<Curso> FindByIdAsync(int id);
    Task<IEnumerable<Curso>> FindByUserIdAsync(int userId);
    void Update(Curso store);
    void Remove(Curso store);
}