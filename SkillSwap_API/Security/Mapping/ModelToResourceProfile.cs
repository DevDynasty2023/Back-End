using AutoMapper;
using SkillSwap_API.Security.Domain.Models;
using SkillSwap_API.Security.Domain.Services.Communication;
using SkillSwap_API.Security.Resources;


namespace SkillSwap_API.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResource>();
        CreateMap<Role, RoleResource>();
        CreateMap<Role, SaveRoleResource>();
    }
}