using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipesManagerWebClient.Web.Pages.Recipes
{
    public class SearchModel : PageModel
    {
        public int[] Models { get; set; }
        public void OnGet()
        {
            
        }
    }
}
