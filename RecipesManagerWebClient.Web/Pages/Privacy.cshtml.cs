using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Models.CreateDtos;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages;

public class PrivacyModel : PageModel
{
    private readonly ApiClient _apiClient; 
    
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger, ApiClient apiClient)
    {
        _logger = logger;
        _apiClient = apiClient;
    }

    public async Task<IActionResult> OnPostAsync(CategoryCreateDto category)
    {
        var request = new GraphQLRequest 
        { 
            Query = @"
            mutation AddCategory($category: CategoryInput!) {
                addCategory(category: $category) {
                    id
                    name
                }
            }
            ", 
            Variables = new 
            {
                category = category
            }
        }; 

        var response = await _apiClient.QueryAsync<Category>(request, "addCategory");

        return RedirectToPage();
    }
}

