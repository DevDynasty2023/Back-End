using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Business.Domain.Services;
using SkillSwap_API.Business.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap_API.Business.Controllers;

[ApiController]
[Route("/api/v1/cursos/{cursoId}/lecciones")]
public class CursoLeccionesController : ControllerBase
{
    private readonly ILeccionService _leccionService;
    private readonly IMapper _mapper;

    public CursoLeccionesController(ILeccionService leccionService, IMapper mapper)
    {
        _leccionService = leccionService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Lecciones Images for given Curso",
        Description = "Get existing Lecciones associated with the specified Curso",
        OperationId = "GetCursoLecciones",
        Tags = new[] { "Cursos"}
    )]
    public async Task<IEnumerable<LeccionResource>> GetAllByCursoIdAsync(int cursoId)
    {
        var lecciones = await _leccionService.ListByCursoIdAsync(cursoId);

        var resources = _mapper.Map<IEnumerable<Leccion>, IEnumerable<LeccionResource>>(lecciones);

        return resources;
    }
}