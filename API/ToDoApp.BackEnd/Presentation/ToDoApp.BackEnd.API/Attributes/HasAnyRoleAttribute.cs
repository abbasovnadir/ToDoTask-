using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos.JWTDto;

namespace ToDoApp.BackEnd.API.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class HasAnyRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles;

    public HasAnyRoleAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var tokenPayload = context.HttpContext.Items["TokenPayload"] as TokenPayloadDto;

        if (tokenPayload == null || tokenPayload.Roles == null || !_roles.Any(r => tokenPayload.Roles.Contains(r)))
        {
            context.Result = new ForbidResult();
        }
    }
}

