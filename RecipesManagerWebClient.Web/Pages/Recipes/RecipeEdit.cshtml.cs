using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages.Recipes
{
    public class RecipeEditModel : PageBase
    {
        private readonly ApiClient _apiClient;

        public Recipe Recipe { get; set; }
        public List<Category> Categories { get; set; }

        public RecipeEditModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var recipe = Request.Query["recipe"];
            var type = Request.Query["type"];

            var request = new GraphQLRequest
            {
                Query = @"query Query($recipeId: String!, $pageNumber: Int!, $pageSize: Int!) {
                          recipe(id: $recipeId) {
                            id
                            name
                            thumbnail {
                              originalPhotoGuid
                              extension
                              imageUploadState
                            }
                            ingredients {
                              name
                              units
                              amount
                            }
                            calories
                            categories {
                              id
                              name
                            }
                            ingredientsText
                            isSaved
                            minutesToCook
                            servingsCount
                            text
                            createdById
                          }
                          categories(pageNumber: $pageNumber, pageSize: $pageSize) {
                            hasPreviousPage
                            hasNextPage
                            pageSize
                            pageNumber
                            items {
                              id
                              name
                            }
                          }
                        }",
                Variables = new
                {
                    recipeId = id,
                    pageNumber = 1,
                    pageSize = 10,
                }
            };

            var response = await _apiClient.QueryAsync(request);
            var jsonResponse = JsonConvert.SerializeObject(response.Data.recipe);
            Recipe = JsonConvert.DeserializeObject<Recipe>(jsonResponse);

            var jsonCategoriesResponse = JsonConvert.SerializeObject(response.Data.categories.items);
            this.Categories = JsonConvert.DeserializeObject<List<Category>>(jsonCategoriesResponse);

            if (Recipe == null)
            {
                return NotFound();
            }

            return Page();
        }

        /*public async Task<IActionResult> OnPostAsync()
        {
            // Get the updated recipe data from the form
            var updatedRecipe = Recipe; // Assuming the updated recipe is already bound to the Recipe property

            // Fetch the existing recipe from the database
            /*var existingRecipe = await _apiClient.Recipe.FindAsync(updatedRecipe.Id);

            if (existingRecipe == null)
            {
                return NotFound();
            }

            // Update the existing recipe with the updated values
            existingRecipe.Name = updatedRecipe.Name;
            existingRecipe.Calories = updatedRecipe.Calories;
            existingRecipe.MinutesToCook = updatedRecipe.MinutesToCook;
            existingRecipe.Ingredients = updatedRecipe.Ingredients;
            existingRecipe.Steps = updatedRecipe.Steps;

            // Save the changes to the database
            await _apiClient.SaveChangesAsync();

            return RedirectToPage("/Recipes/RecipeDetails", new { id = existingRecipe.Id }); 
        }*/
    }

}
