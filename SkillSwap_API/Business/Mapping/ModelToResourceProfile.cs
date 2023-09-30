using AutoMapper;
using SkillSwap_API.Business.Domain.Models;
using SkillSwap_API.Business.Resources;

namespace SkillSwap_API.Business.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Curso, CursoResource>();
        CreateMap<Leccion, LeccionResource>();
    }
}