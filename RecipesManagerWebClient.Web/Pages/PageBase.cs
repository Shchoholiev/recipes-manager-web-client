using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipesManagerWebClient.Web.Pages
{
    public class PageBase : PageModel
    {
        public string ImageUrl { get; set; } = "https://recipes.l7l2.c16.e2-2.dev/";
    }
}
