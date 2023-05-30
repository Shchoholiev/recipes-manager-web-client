using GraphQL;
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

        private readonly ApiClient _apiClient;

        private readonly AuthenticationService _authenticationService;

        public UserEditModel(ApiClient apiClient, AuthenticationService authenticationService)
        {
            _apiClient = apiClient;
            _authenticationService = authenticationService;
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
            // var request = new GraphQLRequest
            // {
            //     Query = @"
            //         mutation UpdateUser($userDto: UserDtoInput!) {
            //             updateUser(userDto: $userDto) {
            //                 tokens {
            //                     accessToken
            //                     refreshToken
            //                 }
            //             }
            //         }
            //     ",
            //     Variables = new
            //     {
            //         userDto = user
            //     }
            // };
            // var response = await _apiClient.QueryAsync(request);

            // if (response?.data?.tokens != null)
            // {
            //     _authenticationService.SetTokens(response.data.tokens);
            // }

            return RedirectToPage("/Profile/user");
        }

    }
}
