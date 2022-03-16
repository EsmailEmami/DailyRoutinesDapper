using System;
using System.Linq;
using System.Security.Claims;

namespace DailyRoutines.Application.Extensions;

public static class IdentityUserExtension
{
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal == null)
            return default;


        try
        {
            return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value.ToGuid();
        }
        catch 
        {
            return default;
        }
    }
}