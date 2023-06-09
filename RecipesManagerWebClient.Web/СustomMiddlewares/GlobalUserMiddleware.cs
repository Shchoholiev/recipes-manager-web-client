﻿using RecipesManagerWebClient.Web.Models.GlobalInstances;
using RecipesManagerWebClient.Web.Network;
using System.Security.Authentication;

namespace RecipesManagerWebClient.Web.CustomMiddlewares;

public class GlobalUserMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalUserMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, AuthenticationService authenticationService, ApiClient apiClient)
    {
        try
        {
            var accessToken = await authenticationService.GetAuthTokenAsync();
            if (!string.IsNullOrEmpty(accessToken))
            {
                apiClient.JwtToken = accessToken;
                GlobalUser.Roles = authenticationService.GetRolesFromJwtToken(accessToken);
                GlobalUser.Id = authenticationService.GetIdFromJwtToken(accessToken);
                GlobalUser.Email = authenticationService.GetEmailFromJwtToken(accessToken);
                GlobalUser.Phone = authenticationService.GetPhoneFromJwtToken(accessToken);
                GlobalUser.Name = authenticationService.GetNameFromJwtToken(accessToken);
            }
        }
        catch (AuthenticationException ex)
        {
            httpContext.Response.Cookies.Delete("accessToken");
            httpContext.Response.Redirect("/login");
        }

        await _next(httpContext);
    }
}