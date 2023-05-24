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
        var request = new GraphQLRequest 
        { 
            Query = @"
            query Recipe($recipeId: String!) {
                recipe(id: $recipeId) {
                    id
                    name
                    ingredients {
                    name
                    }
                    thumbnail {
                        imageUploadState
                        id
                        originalPhotoGuid
                        smallPhotoGuid
                        extension
                    }
                    ingredientsText
                    categories {
                    name
                    id
                    }
                    calories
                    servingsCount
                    isPublic
                    isSaved
                    createdById
                    createdDateUtc
                }
            }", 
            Variables = new 
            {
                recipeId = "645d32a6537ef8eec90db9f4"
            }
        }; 

        var response = await _apiClient.QueryAsync<Recipe>(request, "recipe");
    }

    public async Task<IActionResult> OnPostAsync(IFormCollection form)
    {
        var response = await _apiClient.PostFormAsync<Recipe>("recipes", form);

        return RedirectToPage("/Privacy");
    }
}
