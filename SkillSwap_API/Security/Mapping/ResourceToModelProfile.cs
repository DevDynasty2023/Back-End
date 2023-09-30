using AutoMapper;
using SkillSwap_API.Security.Domain.Models;
using SkillSwap_API.Security.Domain.Services.Communication;
using SkillSwap_API.Security.Resources;


namespace SkillSwap_API.Security.Mapping;

public class ResourceToModelProfile: Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<RegisterRequest, User>();
        CreateMap<SaveRoleResource, Role>();
        CreateMap<UpdateRequest, User>()
            .ForAllMembers(options => options.Condition(
                (source, target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }
            ));
    }
}