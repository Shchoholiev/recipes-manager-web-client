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

        private readonly AuthenticationService _authenticationService;

        public LoginModel(AuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Login.IsEmailOrPhoneProvided)
                {
                    try
                    {
                        await _authenticationService.LoginAsync(Login);
                        return RedirectToPage("/recipes/search");
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = "Login failed. Please try again.";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Please provide an email or phone number.";
                }
            }

            return Page();
        }
    }
}