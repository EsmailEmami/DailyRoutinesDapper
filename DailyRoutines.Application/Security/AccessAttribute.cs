using DailyRoutines.Application.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace DailyRoutines.Application.Security;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class AccessAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {

        if (context.HttpContext.User.Identity is {IsAuthenticated: true})
        {

        }
        else
        {
            context.Result = JsonResponseStatus.Error();
        }
    }
}
