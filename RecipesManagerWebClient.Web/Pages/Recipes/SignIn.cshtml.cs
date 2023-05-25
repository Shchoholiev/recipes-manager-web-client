using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace RecipesManagerWebClient.Web.Pages
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        public SignInInputModel SignIn { get; set; }
        public void OnGet()
        {
            // Perform any necessary initialization
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Check if the user exists based on the login credentials
                bool userExists = CheckUserExistence(SignIn.Email, SignIn.Password);

                if (userExists)
                {
                    // Redirect to the next page
                    return RedirectToPage("/Search");
                }
                else
                {
                    // Display an error message or handle the invalid credentials case
                    ModelState.AddModelError(string.Empty, "Invalid login credentials");
                }
            }

            // If the model state is invalid, redisplay the page with validation errors
            return Page();
        }

        private bool CheckUserExistence(string email, string password)
        {
            // Implement your logic to check if the user exists
            // You can use a database query or any other method to validate the user credentials
            // Return true if the user exists, otherwise return false
            // Example:
            return (email == "example@example.com" && password == "password");
        }
    }
}


public class SignInInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
