using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SkillSwap_API.Security.Domain.Models;

namespace SkillSwap_API.Security.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAprendizAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        /*// If action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        // Then skip authorization process
        if (allowAnonymous)
            return;*/

        // Authorization process
        var info = (String)context.HttpContext.Items["Info"];
        if (info != "null")
        {
            context.Result = info == "Expired Token" ? 
                new JsonResult(new { message = "Expired Token" }) { StatusCode = StatusCodes.Status401Unauthorized } : 
                new JsonResult(new { message = "Invalid Token" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        else
        {
            var user = (User)context.HttpContext.Items["Aprendiz"];
            if (user == null) 
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            else if(user.Role.Name is "Aprendiz" or "Tutor" or "Admin") { var proceed = true; } 
            //Tutor user and Admin user has the same auths as Aprendiz user
        }
        
    }
}