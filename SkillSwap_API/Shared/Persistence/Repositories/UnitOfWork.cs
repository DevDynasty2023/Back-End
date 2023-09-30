using SkillSwap_API.Business.Domain.Repositories;
using SkillSwap_API.Shared.Persistence.Contexts;

namespace SkillSwap_API.Shared.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
private readonly AppDbContext _context;

public UnitOfWork(AppDbContext context)
{
    _context = context;
}

public async Task CompleteAsync()
{
    await _context.SaveChangesAsync();
}
}