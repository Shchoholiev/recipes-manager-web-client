using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesManagerWebClient.Web.Models.Input;
using RecipesManagerWebClient.Web.Network;
using System.ComponentModel.DataAnnotations;

namespace RecipesManagerWebClient.Web.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginInputModel Login { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost(AuthenticationService authenticationService)
        {
            if (ModelState.IsValid)
            {
                authenticationService
            }

            return Page();
        }
    }
}
