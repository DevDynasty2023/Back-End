using SkillSwap_API.Business.Domain.Models;

namespace SkillSwap_API.Business.Domain.Repositories;

public interface ILeccionRepository
{
    Task<IEnumerable<Leccion>> ListAsync();
    Task<Leccion> FindByIdAsync(int id);
    Task<IEnumerable<Leccion>> FindByCursoIdAsync(int cursoId);
    Task AddAsync(Leccion leccion);
    void Update(Leccion leccion);
    void Remove(Leccion leccion);
}