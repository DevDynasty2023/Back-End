using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SkillSwap_API.Security.Domain.Models;

namespace SkillSwap_API.Security.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAdminAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Authorization process
        var info = (String)context.HttpContext.Items["Info"];
        if (info != "null")
        {
            context.Result = info == "Expired Token" ? 
                new JsonResult(new { message = "Expireddd Token" }) { StatusCode = StatusCodes.Status401Unauthorized } : 
                new JsonResult(new { message = "Invalid Token" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        else
        {
            var user = (User)context.HttpContext.Items["User"];
            if (user == null || user.Role.Name == "Aprendiz" || user.Role.Name == "Tutor") //If there is no
                                                                                           //user or the user has role
                                                                                           //of Aprendiz or Tutor
                context.Result = new JsonResult(new { message = "Unauthorized" }) 
                    { StatusCode = StatusCodes.Status401Unauthorized };
        }
        
    }
}