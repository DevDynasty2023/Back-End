using Swashbuckle.AspNetCore.Annotations;

namespace SkillSwap_API.Security.Resources;

public class RoleResource
{
    [SwaggerSchema("Role Identifier")]
    public int Id { get; set; }
    [SwaggerSchema("Role Name")]
    public string Name { get; set; }
}