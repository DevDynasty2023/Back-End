namespace SkillSwap_API.Business.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}