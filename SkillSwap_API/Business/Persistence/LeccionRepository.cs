using Microsoft.EntityFrameworkCore;
using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Business.Domain.Repositories;
using SkillSwap_API.Shared.Persistence.Contexts;
using SkillSwap_API.Shared.Persistence.Repositories;

namespace SkillSwap_API.Business.Persistence;

public class LeccionRepository : BaseRepository, ILeccionRepository
{
    public LeccionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Leccion>> ListAsync()
    {
        return await _context.Lecciones.ToListAsync();
    }

    public async Task<Leccion> FindByIdAsync(int id)
    {
        return await _context.Lecciones.FindAsync(id);
    }

    public async Task<IEnumerable<Leccion>> FindByCursoIdAsync(int cursoId)
    {
        return await _context.Lecciones
            .Where(p => p.CursoId == cursoId)
            //.Include(p => p.Curso)
            .ToListAsync();
    }

    public async Task AddAsync(Leccion leccion)
    {
        await _context.Lecciones.AddAsync(leccion);
    }

    public void Update(Leccion leccion)
    {
        _context.Lecciones.Update(leccion);
    }

    public void Remove(Leccion leccion)
    {
        _context.Lecciones.Remove(leccion);
    }
}