using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Business.Domain.Services;
using SkillSwap_API.Business.Resources;
using SkillSwap_API.Security.Authorization.Attributes;
using SkillSwap_API.Shared.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap_API.Business.Controllers;
[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete Cursos")]
public class CursosController : ControllerBase
{
    private readonly ICursoService _cursoService;
    private readonly IMapper _mapper;
    
    
    public CursosController(ICursoService cursoService, IMapper mapper)
    {
        _cursoService = cursoService;
        _mapper = mapper;
    }
    
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CursoResource>), 200)]
    public async Task<IEnumerable<CursoResource>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<Curso>, 
            IEnumerable<CursoResource>>(await _cursoService.ListAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var store = await _cursoService.GetByIdAsync(id);
        var resource = _mapper.Map<Curso, CursoResource>(store);
        return Ok(resource);
    }
    
    [AuthorizeTutor]
    [HttpPost]
    [ProducesResponseType(typeof(CursoResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveCursoResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var storeModel = _mapper.Map<SaveCursoResource, Curso>(resource);
        
        var storeResponse = await _cursoService.SaveAsync(storeModel);
        
        if (!storeResponse.Success)
            return BadRequest(storeResponse.Message);
        
        var storeResource = _mapper.Map<Curso, CursoResource>(storeResponse.Model);
        
        return Ok(storeResource);
    }
    
    [AuthorizeTutor]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCursoResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var store = _mapper.Map<SaveCursoResource, Curso>(resource);

        var storeResponse = await _cursoService.UpdateAsync(id, store); 

        if (!storeResponse.Success)
            return BadRequest(storeResponse.Message);

        var storeResource = _mapper.Map<Curso, CursoResource>(storeResponse.Model);

        return Ok(storeResource);
    }
    
    [AuthorizeTutor]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _cursoService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var tutorialResource = _mapper.Map<Curso, CursoResource>(result.Model);

        return Ok(tutorialResource);
    }

}