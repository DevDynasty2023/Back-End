using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Business.Domain.Services.Communication;

namespace SkillSwap_API.Business.Domain.Services;

public interface ILeccionService
{
    Task<IEnumerable<Leccion>> ListAsync();
    Task<Leccion> GetByIdAsync(int id);
    Task<IEnumerable<Leccion>> ListByCursoIdAsync(int cursoId);
    Task<LeccionResponse> SaveAsync(Leccion leccion);
    Task<LeccionResponse> UpdateAsync(int id, Leccion leccion);
    Task<LeccionResponse> DeleteAsync(int leccionId);
}