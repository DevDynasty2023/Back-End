using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Business.Domain.Repositories;
using SkillSwap_API.Business.Domain.Services;
using SkillSwap_API.Business.Domain.Services.Communication;
using SkillSwap_API.Security.Domain.Repositories;
using SkillSwap_API.Security.Exceptions;

namespace SkillSwap_API.Business.Services;

public class CursoService : ICursoService
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CursoService(ICursoRepository cursoRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Curso>> ListAsync()
    {
        return await _cursoRepository.ListAsync();
    }

    public async Task<Curso> GetByIdAsync(int id)
    {
        var curso = await _cursoRepository.FindByIdAsync(id);
        if (curso == null) throw new KeyNotFoundException("Curso not found");
        return curso;
    }

    public async Task<IEnumerable<Curso>> ListByUserIdAsync(int userId)
    {
        var existingUser = await _userRepository.FindByIdAsync(userId);

        if (existingUser == null)
            throw new AppException("Invalid User");

        return await _cursoRepository.FindByUserIdAsync(userId);
    }

    public async Task<CursoResponse> SaveAsync(Curso curso)
    {
        var existingUser = await _userRepository.FindByIdAsync(curso.UserId);
        if (existingUser == null)
            return new CursoResponse("Invalid User");

        try
        {
            await _cursoRepository.AddAsync(curso);
            await _unitOfWork.CompleteAsync();
            return new CursoResponse(curso);
        }
        catch (Exception e)
        {
            return new CursoResponse($"An error occurred while saving the course: {e.Message}");
        }
    }

    public async Task<CursoResponse> UpdateAsync(int cursoId, Curso curso)
    {
        var existingCurso = await _cursoRepository.FindByIdAsync(cursoId);

        if (existingCurso == null)
            return new CursoResponse("Curso not found.");

        var existingUser = await _userRepository.FindByIdAsync(curso.UserId);
        if (existingUser == null)
            return new CursoResponse("Invalid User");

        // Modify Fields
        existingCurso.Name = curso.Name;
        existingCurso.Description = curso.Description;
        existingCurso.UserId = curso.UserId;

        try
        {
            _cursoRepository.Update(existingCurso);
            await _unitOfWork.CompleteAsync();

            return new CursoResponse(existingCurso);

        }
        catch (Exception e)
        {
            // Error Handling
            return new CursoResponse($"An error occurred while updating the curso: {e.Message}");
        }
    }

    public async Task<CursoResponse> DeleteAsync(int storeId)
    {
        var existingCurso = await _cursoRepository.FindByIdAsync(storeId);

        if (existingCurso == null)
            return new CursoResponse("Curso not found.");

        try
        {
            _cursoRepository.Remove(existingCurso);
            await _unitOfWork.CompleteAsync();

            return new CursoResponse(existingCurso);

        }
        catch (Exception e)
        {
            // Error Handling
            return new CursoResponse($"An error occurred while deleting the curso: {e.Message}");
        }
    }
}
