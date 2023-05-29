using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Models.CreateDtos;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly ApiClient _apiClient; 

    public IndexModel(ILogger<IndexModel> logger,
        ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public async Task OnGetAsync()
    {
 
    }

    public async Task<IActionResult> OnPostAsync(IFormCollection form)
    {
        var response = await _apiClient.PostFormAsync<Recipe>("recipes", form);

        return RedirectToPage("/user");
    }
}
