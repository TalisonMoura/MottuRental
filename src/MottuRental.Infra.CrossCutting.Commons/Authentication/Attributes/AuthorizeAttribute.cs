using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MottuRental.Infra.CrossCutting.Commons.Authentication.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute(string? roles = null) : Attribute, IAuthorizationFilter
{
    public readonly IEnumerable<string?> Roles = roles?.Split(",") ?? null;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException("Unauthorized");

        if (Roles is null)
            return;

        var user = context.HttpContext.User;
        var roles = user.FindAll(ClaimTypes.Role).Select(claim => claim.Value).ToList();
        var isAdmin = roles.Contains("Manager");
        var authorizedRoles = Roles.Intersect(roles);

        if (authorizedRoles.Any() || isAdmin)
            return;

        throw new UnauthorizedAccessException("Unauthorized");
    }
}
