using Newtonsoft.Json.Linq;
using RecipesManagerWebClient.Web.Models.GlobalInstances;
using RecipesManagerWebClient.Web.Network;
using System.Security.Claims;

namespace RecipesManagerWebClient.Web.СustomMiddlewares;

public class GlobalUserMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalUserMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, AuthenticationService authenticationService)
    {
        var accessToken = await authenticationService.GetAuthTokenAsync();

        if (!string.IsNullOrEmpty(accessToken))
        {
            GlobalUser.Roles = authenticationService.GetRolesFromJwtToken(accessToken);
            GlobalUser.Id = authenticationService.GetIdFromJwtToken(accessToken);
            GlobalUser.Email = authenticationService.GetEmailFromJwtToken(accessToken);
            GlobalUser.Phone = authenticationService.GetPhoneFromJwtToken(accessToken);
            GlobalUser.Name = authenticationService.GetNameFromJwtToken(accessToken);
        }

        await _next(httpContext);
    }
}