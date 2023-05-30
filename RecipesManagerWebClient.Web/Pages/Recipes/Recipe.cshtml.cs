using System.Reactive;
using System.Threading.Tasks;
using System.Xml.Linq;
using GraphQL.Client.Http;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages.Recipes
{
    public class RecipeModel : PageBase
    {
        private readonly ApiClient _apiClient;

        public Recipe Recipe { get; set; }
        public List<Category> Categories { get; set; }



        public RecipeModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var type = Request.Query["type"];
            var id = Request.Query["recipeId"].ToString();
        


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
                            createdBy {
                              name
                              id
                              webId
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

    }
}

