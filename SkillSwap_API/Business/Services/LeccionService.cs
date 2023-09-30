using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Business.Domain.Repositories;
using SkillSwap_API.Business.Domain.Services;
using SkillSwap_API.Business.Domain.Services.Communication;
using SkillSwap_API.Security.Exceptions;

namespace SkillSwap_API.Business.Services;

public class LeccionService : ILeccionService
{
    private readonly ILeccionRepository _leccionRepository;
    private readonly ICursoRepository _cursoRepository;
    private readonly IUnitOfWork _unitOfWork;


    public LeccionService(ILeccionRepository leccionRepository, ICursoRepository cursoRepository, IUnitOfWork unitOfWork)
    {
        _leccionRepository = leccionRepository;
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Leccion>> ListAsync()
    {
        return await _leccionRepository.ListAsync();
    }

    public async Task<Leccion> GetByIdAsync(int id)
    {
        var leccion = await _leccionRepository.FindByIdAsync(id);
        if (leccion == null) throw new KeyNotFoundException("Leccion not found");
        return leccion;
    }

    public async Task<IEnumerable<Leccion>> ListByCursoIdAsync(int cursoId)
    {
        var existingCurso = await _cursoRepository.FindByIdAsync(cursoId);

        if (existingCurso == null)
            throw new AppException("Invalid Curso");

        return await _leccionRepository.FindByCursoIdAsync(cursoId);
    }

    public async Task<LeccionResponse> SaveAsync(Leccion leccion)
    {
        var existingCurso = await _cursoRepository.FindByIdAsync(leccion.CursoId);
        if (existingCurso == null)
            return new LeccionResponse("Invalid Curso");

        try
        {
            await _leccionRepository.AddAsync(leccion);
            await _unitOfWork.CompleteAsync();
            return new LeccionResponse(leccion);
        }
        catch (Exception e)
        {
            return new LeccionResponse($"An error occurred while saving the leccion: {e.Message}");
        }
    }

    public async Task<LeccionResponse> UpdateAsync(int id, Leccion leccion)
    {
        var existingLeccion = await _leccionRepository.FindByIdAsync(id);

        if (existingLeccion == null)
            return new LeccionResponse("Leccion not found.");

        var existingCurso = await _cursoRepository.FindByIdAsync(leccion.CursoId);
        if (existingCurso == null)
            return new LeccionResponse("Invalid Curso");

        // Modify Fields
        existingLeccion.titulo = leccion.titulo;
        existingLeccion.contenido = leccion.contenido;
        existingLeccion.CursoId = leccion.CursoId;

        try
        {
            _leccionRepository.Update(existingLeccion);
            await _unitOfWork.CompleteAsync();

            return new LeccionResponse(existingLeccion);

        }
        catch (Exception e)
        {
            // Error Handling
            return new LeccionResponse($"An error occurred while updating the leccion: {e.Message}");
        }
    }

    public async Task<LeccionResponse> DeleteAsync(int leccionId)
    {
        var existingLeccion = await _leccionRepository.FindByIdAsync(leccionId);

        if (existingLeccion == null)
            return new LeccionResponse("Leccion not found.");

        try
        {
            _leccionRepository.Remove(existingLeccion);
            await _unitOfWork.CompleteAsync();

            return new LeccionResponse(existingLeccion);

        }
        catch (Exception e)
        {
            // Error Handling
            return new LeccionResponse($"An error occurred while deleting the leccion: {e.Message}");
        }
    }
}