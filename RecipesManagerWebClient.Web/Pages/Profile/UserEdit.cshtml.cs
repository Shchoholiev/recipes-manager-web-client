using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Models.GlobalInstances;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages.Profile
{
    public class UserEditModel : PageBase
    {
        public User User { get; private set; }

        private readonly ILogger<IndexModel> _logger;

        private readonly ApiClient _apiClient;

        public UserEditModel(ILogger<IndexModel> logger,
           ApiClient apiClient)
            {
                _logger = logger;
                _apiClient = apiClient;
            }

        public async Task OnGetAsync()
        {
            User = new User
            {
                Name = GlobalUser.Name,
                Id = GlobalUser.Id,
                Email = GlobalUser.Email,
                Phone = GlobalUser.Phone,
                
            };
        }

        public async Task<IActionResult> OnPostAsync(User user)
        {
            //var response = await _apiClient.QueryAsync<User>("users", form);

            return RedirectToPage("/Profile/user");


        }

    }
}
