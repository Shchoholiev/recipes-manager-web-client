using RecipesManagerWebClient.Web.СustomMiddlewares;

namespace RecipesManagerWebClient.Web;

public static class Extentions
{
    public static IApplicationBuilder ConfogureGlobalUserMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalUserMiddleware>();
        return app;
    }
}
