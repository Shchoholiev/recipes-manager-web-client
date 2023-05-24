using GraphQL.Client.Http;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages.Recipes
{
    public class SearchResultModel : PageBase
    {
        public List<Recipe> Recipes { get; set; }
        public List<Category> Categories { get; set; }

        private readonly ApiClient _apiClient;

        public SearchResultModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task OnGetAsync()
        {
            var search = Request.Query["search"];
            var request = new GraphQLRequest
            {
                Query = @"query SearchRecipes($recipeSearchType: RecipesSearchTypes!, $categoriesIds: [String!], $pageNumber: Int!, $pageSize: Int!, $searchString: String!, $authorId: String!, $categoriesPageNumber2: Int!, $categoriesPageSize2: Int!) {
                  searchRecipes(recipeSearchType: $recipeSearchType, categoriesIds: $categoriesIds, pageNumber: $pageNumber, pageSize: $pageSize, searchString: $searchString, authorId: $authorId) {
                    hasNextPage
                    hasPreviousPage
                    items {
                      categories {
                        id
                        name
                      }
                      createdBy {
                        id
                        name
                      }
                      id
                      isSaved
                      minutesToCook
                      name
                      thumbnail {
                        smallPhotoGuid
                        imageUploadState
                        extension
                      }
                    }
                  }
                  categories(pageNumber: $categoriesPageNumber2, pageSize: $categoriesPageSize2) {
                    pageNumber
                    pageSize
                    items {
                      name
                    }
                  }
                }",

                Variables = new
                {
                    recipeSearchType = "PUBLIC",
                    pageNumber = 1,
                    pageSize = 10,
                    categoriesIds = new string[0],
                    searchString = search.FirstOrDefault() ?? string.Empty,
                    authorId = "",
                    categoriesPageNumber2 = 1,
                    categoriesPageSize2 = 10
                }
            };

            var response = await _apiClient.QueryAsync(request);
            var jsonResponse = JsonConvert.SerializeObject(response.Data.searchRecipes.items);
            this.Recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonResponse);

            var jsonCategoriesResponse = JsonConvert.SerializeObject(response.Data.categories.items);
            this.Categories = JsonConvert.DeserializeObject<List<Category>>(jsonCategoriesResponse);

        }

    }
}
