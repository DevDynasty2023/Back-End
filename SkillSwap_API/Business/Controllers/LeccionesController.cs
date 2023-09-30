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
[SwaggerTag("Create, read, update and delete Lecciones")] 
public class LeccionesController : ControllerBase
{
    private readonly ILeccionService _leccionService;
    private readonly IMapper _mapper;
    
    
    public LeccionesController(ILeccionService leccionService, IMapper mapper)
    {
        _leccionService = leccionService;
        _mapper = mapper;
    }
    
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LeccionResource>), 200)]
    public async Task<IEnumerable<LeccionResource>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<Leccion>, 
            IEnumerable<LeccionResource>>(await _leccionService.ListAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var store = await _leccionService.GetByIdAsync(id);
        var resource = _mapper.Map<Leccion, LeccionResource>(store);
        return Ok(resource);
    }
    
    [AuthorizeTutor]
    [HttpPost]
    [ProducesResponseType(typeof(LeccionResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> PostAsync([FromBody] SaveLeccionResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var storeModel = _mapper.Map<SaveLeccionResource, Leccion>(resource);
        
        var storeResponse = await _leccionService.SaveAsync(storeModel);
        
        if (!storeResponse.Success)
            return BadRequest(storeResponse.Message);
        
        var storeResource = _mapper.Map<Leccion, LeccionResource>(storeResponse.Model);
        
        return Ok(storeResource);
    }
    
    [AuthorizeTutor]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveLeccionResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var store = _mapper.Map<SaveLeccionResource, Leccion>(resource);

        var storeResponse = await _leccionService.UpdateAsync(id, store); 

        if (!storeResponse.Success)
            return BadRequest(storeResponse.Message);

        var storeResource = _mapper.Map<Leccion, LeccionResource>(storeResponse.Model);

        return Ok(storeResource);
    }
    
    [AuthorizeTutor]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _leccionService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var tutorialResource = _mapper.Map<Leccion, LeccionResource>(result.Model);

        return Ok(tutorialResource);
    }
}