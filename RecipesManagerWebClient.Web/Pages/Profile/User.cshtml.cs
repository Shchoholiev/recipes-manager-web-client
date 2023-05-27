using GraphQL;
using Newtonsoft.Json;
using RecipesManagerWebClient.Web.Models;
using RecipesManagerWebClient.Web.Network;

namespace RecipesManagerWebClient.Web.Pages.Profile
{
    public class UserModel : PageBase
    {
        public List<Recipe> Recipes { get; set; }
        public List<Category> Categories { get; set; }
        public User User { get; set; }


        private readonly ApiClient _apiClient;

        public UserModel(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task OnGetAsync()
        {
            var search = Request.Query["search"];
            var request = new GraphQLRequest
            {
                Query = @"query CurrentUser($recipeSearchType: RecipesSearchTypes!, $pageNumber: Int!, $pageSize: Int!, $categoriesPageNumber2: Int!, $categoriesPageSize2: Int!) {
                  currentUser {
                    id
                    name
                  }
                  searchRecipes(recipeSearchType: $recipeSearchType, pageNumber: $pageNumber, pageSize: $pageSize) {
                    hasNextPage
                    hasPreviousPage
                    pageNumber
                    pageSize
                    totalItems
                    totalPages
                    items {
                      createdById
                      id
                      isPublic
                      isSaved
                      minutesToCook
                      name
                      thumbnail {
                        extension
                        imageUploadState
                        smallPhotoGuid
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
                    recipeSearchType = "PERSONAL",
                    pageNumber = 1,
                    pageSize = 12,
                    categoriesPageNumber2 = 1,
                    categoriesPageSize2 = 10
                }
            };
            var response = await _apiClient.QueryAsync(request);

            var jsonResponse = JsonConvert.SerializeObject(response.Data.searchRecipes.items);
            this.Recipes = JsonConvert.DeserializeObject<List<Recipe>>(jsonResponse);

            var jsonCategoriesResponse = JsonConvert.SerializeObject(response.Data.categories.items);
            this.Categories = JsonConvert.DeserializeObject<List<Category>>(jsonCategoriesResponse);
            
            var jsonUserResponse = response.Data.currentUser.ToString();
            this.User = JsonConvert.DeserializeObject<User>(jsonUserResponse);
        }
    }
}
