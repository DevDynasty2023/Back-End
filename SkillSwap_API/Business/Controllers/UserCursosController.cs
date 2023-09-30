using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Business.Domain.Services;
using SkillSwap_API.Business.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap_API.Business.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/cursos")]
public class UserCursosController : ControllerBase
{
    private readonly ICursoService _cursoService;
    private readonly IMapper _mapper;

    public UserCursosController(ICursoService cursoService, IMapper mapper)
    {
        _cursoService = cursoService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Cursos for given User",
        Description = "Get existing Cursos associated with the specified User",
        OperationId = "GetUserCursos",
        Tags = new[] { "Users"}
    )]
    public async Task<IEnumerable<CursoResource>> GetAllByUserIdAsync(int userId)
    {
        var cursos = await _cursoService.ListByUserIdAsync(userId);

        var resources = _mapper.Map<IEnumerable<Curso>, IEnumerable<CursoResource>>(cursos);

        return resources;
    }
}