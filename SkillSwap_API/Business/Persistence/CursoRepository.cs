using Microsoft.EntityFrameworkCore;
using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Business.Domain.Repositories;
using SkillSwap_API.Shared.Persistence.Contexts;
using SkillSwap_API.Shared.Persistence.Repositories;

namespace SkillSwap_API.Business.Persistence;

public class CursoRepository : BaseRepository, ICursoRepository
{
    public CursoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Curso>> ListAsync()
    {
        return await _context.Cursos.ToListAsync();
    }

    public async Task AddAsync(Curso store)
    {
        await _context.Cursos.AddAsync(store);
    }

    public async Task<Curso> FindByIdAsync(int id)
    {
        return await _context.Cursos.FindAsync(id);
    }

    public async Task<IEnumerable<Curso>> FindByUserIdAsync(int userId)
    {
        return await _context.Cursos
            .Where(p => p.UserId == userId)
            //.Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Curso store)
    {
        _context.Cursos.Update(store);
    }

    public void Remove(Curso store)
    {
        _context.Cursos.Remove(store);
    }
}