using System.Security.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using RecipesManagerWebClient.Web.CustomMiddlewares;

namespace RecipesManagerWebClient.Web;

public static class Extentions
{
    public static IApplicationBuilder ConfigureGlobalUserMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalUserMiddleware>();
        return app;
    }

    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                if (exception is AuthenticationException)
                {
                    context.Response.Redirect("/login");
                    return;
                }

                // Other exception handling logic...
            });
        });

        return app;
    }
}
