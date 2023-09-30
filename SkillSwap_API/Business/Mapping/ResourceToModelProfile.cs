using AutoMapper;
using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Business.Resources;

namespace SkillSwap_API.Business.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveCursoResource, Curso>();
        CreateMap<SaveLeccionResource, Leccion>();
    }
}